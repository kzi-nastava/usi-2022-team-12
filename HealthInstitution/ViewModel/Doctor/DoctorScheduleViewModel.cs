using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthInstitution.Commands.doctor;
using HealthInstitution.Commands.doctor.Navigation;
using HealthInstitution.Model.appointment;
using HealthInstitution.Model.user;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.Utility;

namespace HealthInstitution.ViewModel.doctor
{
    public class DoctorScheduleViewModel : ViewModelBase
    {
        public class AppointmentViewModel : ViewModelBase
        {
            private readonly Appointment _appointment;
            public Appointment Appointment => _appointment;
            public Patient Patient => _appointment.Patient;
            public string PatientName => _appointment.Patient.FullName;
            public DateTime Date => _appointment.StartDate;
            public DateTime Time => _appointment.StartDate;
            public string Room => _appointment.Room.Name;
            public bool IsDone => _appointment.IsDone;
            public AppointmentViewModel(Appointment appointment)
            {
                _appointment = appointment;
            }
        }
        private DateTime _userDate;
        public DateTime UserDate
        {
            get => _userDate;
            set
            {
                _userDate = value;
                UpdateData();
                OnPropertyChanged(nameof(UserDate));
            }
        }

        private bool _next3Days;

        public bool Next3Days
        {
            get => _next3Days;
            set
            {
                _next3Days = value;
                UpdateData();
                OnPropertyChanged(nameof(Next3Days));
            }
        }
        private AppointmentViewModel _selectedAppointment;

        public AppointmentViewModel SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                _selectedAppointment = value;
                OnPropertyChanged(nameof(SelectedAppointment));
            }
        }
        private DateTime EndDate
        {
            get
            {
                if (Next3Days)
                {
                    return UserDate.AddDays(3);
                }
                else
                {
                    return UserDate;
                }
            }
        }
        private readonly IAppointmentService _appointmentService;

        public IAppointmentService AppointmentService => _appointmentService;

        private readonly ObservableCollection<AppointmentViewModel> _appointments;

        public IEnumerable<AppointmentViewModel> Appointments => _appointments;

        public ICommand OpenMedicalRecordCommand { get; }
        public ICommand StartAppointmentCommand { get; }
        public ICommand CreateAppointmentCommand { get; }
        public ICommand UpdateAppointmentCommand { get; }
        public ICommand CancelAppointmentCommand { get; }
        public DoctorScheduleViewModel(IAppointmentService appointemntService)
        {
            _userDate = DateTime.Now;
            _appointmentService = appointemntService;
            _appointments = new ObservableCollection<AppointmentViewModel>();
            OpenMedicalRecordCommand = new NavigateMedicalRecordCommand(this);
            StartAppointmentCommand = new StartAppointmentCommand(this);
            CreateAppointmentCommand = new NavigateDoctorAppointmentCreationCommand(this);
            UpdateAppointmentCommand = new NavigateDoctorAppointmentUpdateCommand(this);
            CancelAppointmentCommand = new DoctorCancelAppointmentCommand(this);
            UpdateData();
            EventBus.RegisterHandler("UpdateSchedule", () =>
            {
                UpdateData();
            });
        }
        public void UpdateData()
        {
            DateTime startDate = UserDate;
            DateTime endDate = EndDate;
            Doctor doctor = GlobalStore.ReadObject<Doctor>("LoggedUser");
            IEnumerable<Appointment> appointments = _appointmentService.GetAppointmentsForDateRangeAndDoctor(startDate, endDate, doctor);

            _appointments.Clear();
            foreach (Appointment appointment in appointments)
            {
                _appointments.Add(new AppointmentViewModel(appointment));
            }
            OnPropertyChanged(nameof(Appointments));
        }
    }
}
