using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Core.Features.AppointmentScheduling.Commands.PatientCMD;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.UsersManagement.Commands.PatientCMD;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Service;
using HealthInstitution.Core.Ninject;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.AppointmentScheduling
{
    public class AppointmentCreationViewModel : ViewModelBase
    {
        #region services
        private readonly ISchedulingService _schedulingService;
        private readonly IActivityService _activityService;
        private readonly IPatientRepository _patientRepository;

        public ISchedulingService SchedulingService => _schedulingService;
        public IActivityService ActivityService => _activityService;
        public IPatientRepository PatientRepository => _patientRepository;
        #endregion

        #region attributes
        private DateTime _startDate;
        private DateTime _startTime;
        private Doctor _selectedDoctor;
        private List<Doctor> _doctors;
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
        #endregion

        #region commands
        public ICommand? MakeAppointmentCommand { get; }
        public ICommand? BackCommand { get; }
        #endregion

        public AppointmentCreationViewModel(IDoctorRepository doctorRepository, IPatientRepository patientRepository, ISchedulingService schedulingService, IActivityService activityService)
        {
            _activityService = activityService;
            _schedulingService = schedulingService;
            _patientRepository = patientRepository;

            DateTime currentDateTime = DateTime.Now;
            StartDate = currentDateTime.Date;
            StartTime = currentDateTime.Date.AddHours(currentDateTime.Hour).AddMinutes(currentDateTime.Minute);
            Doctors = doctorRepository.ReadAll().OrderBy(doc => doc.Specialization).ToList();

            MakeAppointmentCommand = new MakeAppointmentCommand(this);
            BackCommand = new PatientAppointmentsCommand();
        }

        public AppointmentCreationViewModel(Doctor selectedDoctor)
        {
            _activityService = ServiceLocator.Get<IActivityService>();
            _schedulingService = ServiceLocator.Get<ISchedulingService>();
            _patientRepository = ServiceLocator.Get<IPatientRepository>();

            DateTime currentDateTime = DateTime.Now;
            StartDate = currentDateTime.Date;
            StartTime = currentDateTime.Date.AddHours(currentDateTime.Hour).AddMinutes(currentDateTime.Minute);
            Doctors = new List<Doctor>() { selectedDoctor };
            SelectedDoctor = selectedDoctor;

            MakeAppointmentCommand = new MakeAppointmentCommand(this);
            BackCommand = new DoctorSearchCommand();
        }
    }
}
