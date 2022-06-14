using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Commands.patient;
using HealthInstitution.Commands.patient.Navigation;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.ViewModel.patient
{
    public class AppointmentUpdateViewModel : ViewModelBase
    {
        #region services
        private readonly IAppointmentService _appointmentService;
        private readonly IActivityService _activityService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public IAppointmentService AppointmentService => _appointmentService;
        public IActivityService ActivityService => _activityService;
        public IPatientService PatientService => _patientService;
        public IDoctorService DoctorService => _doctorService;
        #endregion endregion

        #region attributes
        private DateTime _startDate;
        private DateTime _startTime;
        private Doctor _selectedDoctor;
        private List<Doctor> _doctors;
        private Appointment _selectedAppointment;
        #endregion

        #region properties
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }
        public Doctor SelectedDoctor
        {
            get => _selectedDoctor;
            set
            {
                _selectedDoctor = value;
                OnPropertyChanged(nameof(SelectedDoctor));
            }
        }
        public List<Doctor> Doctors
        {
            get => _doctors;
            set
            {
                _doctors = value;
                OnPropertyChanged(nameof(Doctors));
            }
        }
        public Appointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                _selectedAppointment = value;
                OnPropertyChanged(nameof(SelectedAppointment));
            }
        }
        #endregion

        #region commands
        public ICommand? UpdateAppointmentCommand { get; }
        public ICommand? BackCommand { get; }
        #endregion

        public AppointmentUpdateViewModel(IDoctorService doctorService, IAppointmentService appointmentService, IActivityService activityService, IPatientService patientService)
        {
            SelectedAppointment = GlobalStore.ReadObject<Appointment>("SelectedAppointment");
            _appointmentService = appointmentService;
            _activityService = activityService;
            _patientService = patientService;
            _doctorService = doctorService;
            Doctors = doctorService.ReadAll().OrderBy(doc => doc.Specialization).ToList();

            StartDate = SelectedAppointment.StartDate;
            StartTime = SelectedAppointment.StartDate;
            SelectedDoctor = SelectedAppointment.Doctor;

            UpdateAppointmentCommand = new UpdateAppointmentCommand(this);
            BackCommand = new PatientAppointmentsCommand();
        }
    }
}
