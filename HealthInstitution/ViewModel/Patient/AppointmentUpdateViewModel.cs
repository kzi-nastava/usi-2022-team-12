using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
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
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _startTime;
        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }

        private Doctor _selectedDoctor;
        public Doctor SelectedDoctor
        {
            get => _selectedDoctor;
            set
            {
                _selectedDoctor = value;
                OnPropertyChanged(nameof(SelectedDoctor));
            }
        }

        private List<Doctor> _doctors;
        public List<Doctor> Doctors
        {
            get => _doctors;
            set
            {
                _doctors = value;
                OnPropertyChanged(nameof(Doctors));
            }
        }

        private Appointment _selectedAppointment;
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
