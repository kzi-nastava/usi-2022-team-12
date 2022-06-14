using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Commands.patient;
using HealthInstitution.Commands.patient.Navigation;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.GUI.Utility.ViewModel;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.ViewModel.patient
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
        private DateTime _startTime;
        private DateTime _endTime;
        private string _selectedPriority;
        private Doctor _selectedDoctor;
        private List<Doctor> _doctors;
        private List<Appointment> _recommendedAppointments;
        private Appointment _selectedAppointment;
        #endregion

        #region properties
        public DateTime DeadlineDate
        {
            get => _deadlineDate;
            set
            {
                _deadlineDate = value;
                OnPropertyChanged(nameof(DeadlineDate));
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
        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                OnPropertyChanged(nameof(EndTime));
            }
        }
        public string SelectedPriority
        {
            get => _selectedPriority;
            set
            {
                _selectedPriority = value;
                OnPropertyChanged(nameof(SelectedPriority));
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
        public List<Appointment> RecommendedAppointments
        {
            get => _recommendedAppointments;
            set
            {
                _recommendedAppointments = value;
                OnPropertyChanged(nameof(RecommendedAppointments));
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
