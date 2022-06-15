using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Core.Features.AppointmentScheduling.Commands.PatientCMD;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Service;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.AppointmentScheduling
{
    public class RecommendAppointmentCreationViewModel : ViewModelBase
    {
        #region services
        private readonly IAppointmentRepository _appintmentRepository;
        private readonly ISchedulingService _schedulingService;
        private readonly IActivityService _activityService;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public IAppointmentRepository AppintmentRepository => _appintmentRepository;
        public ISchedulingService SchedulingService => _schedulingService;
        public IActivityService ActivityService => _activityService;
        public IDoctorRepository DoctorRepository => _doctorRepository;
        public IPatientRepository PatientRepository => _patientRepository;
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

        public RecommendAppointmentCreationViewModel(IAppointmentRepository appintmentRepository, ISchedulingService schedulingService, IActivityService activityService, IDoctorRepository doctorRepository, IPatientRepository patientRepository)
        {
            _appintmentRepository = appintmentRepository;
            _schedulingService = schedulingService;
            _activityService = activityService;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;

            DateTime currentDateTime = DateTime.Now;
            DeadlineDate = currentDateTime.Date;
            StartTime = currentDateTime.Date.AddHours(currentDateTime.Hour).AddMinutes(currentDateTime.Minute);
            EndTime = currentDateTime.Date.AddHours(currentDateTime.Hour).AddMinutes(currentDateTime.Minute + 15);

            _selectedPriority = "TimeInterval";
            Doctors = doctorRepository.ReadAll().OrderBy(doc => doc.Specialization).ToList();
            RecommendAppointmentCommand = new RecommendAppointmentCommand(this);
            ConfirmRecommendationCommand = new ConfirmRecommendationCommand(this);
            BackCommand = new PatientAppointmentsCommand();
        }
    }
}
