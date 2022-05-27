using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class RecommendAppointmentCreationViewModel : ViewModelBase
    {
        #region services
        private readonly IAppointmentService _appointmentService;
        private readonly IActivityService _activityService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        public IAppointmentService AppointmentService => _appointmentService;
        public IActivityService ActivityService => _activityService;
        public IDoctorService DoctorService => _doctorService;
        public IPatientService PatientService => _patientService;
        #endregion

        #region attributes
        private DateTime _deadlineDate;
        public DateTime DeadlineDate
        {
            get => _deadlineDate;
            set
            {
                _deadlineDate = value;
                OnPropertyChanged(nameof(DeadlineDate));
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

        private DateTime _endTime;
        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                OnPropertyChanged(nameof(EndTime));
            }
        }

        private string _selectedPriority;
        public string SelectedPriority
        {
            get => _selectedPriority;
            set
            {
                _selectedPriority = value;
                OnPropertyChanged(nameof(SelectedPriority));
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

        private List<Appointment> _recommendedAppointments;
        public List<Appointment> RecommendedAppointments 
        {
            get => _recommendedAppointments;
            set 
            {
                _recommendedAppointments = value;
                OnPropertyChanged(nameof(RecommendedAppointments));
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
        public ICommand RecommendAppointmentCommand { get; }
        public ICommand ConfirmRecommendationCommand { get; }
        public ICommand BackCommand { get; }
        #endregion

        public RecommendAppointmentCreationViewModel(IDoctorService doctorService, IPatientService patientService, IAppointmentService appointmentService, IActivityService activityService)
        {
            _activityService = activityService;
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _patientService = patientService;

            DateTime currentDateTime = DateTime.Now;
            DeadlineDate = currentDateTime.Date;
            StartTime = currentDateTime.Date.AddHours(currentDateTime.Hour).AddMinutes(currentDateTime.Minute);
            EndTime = currentDateTime.Date.AddHours(currentDateTime.Hour).AddMinutes(currentDateTime.Minute + 15);

            _selectedPriority = "TimeInterval";
            Doctors = DoctorService.ReadAll().OrderBy(doc => doc.Specialization).ToList();
            RecommendAppointmentCommand = new RecommendAppointmentCommand(this);
            ConfirmRecommendationCommand = new ConfirmRecommendationCommand(this);
            BackCommand = new PatientAppointmentsCommand();
        }
    }
}
