using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Core.Features.AppointmentScheduling.Commands.PatientCMD;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.AppointmentScheduling
{
    public class PatientAppointmentsViewModel : ViewModelBase
    {
        #region services
        private readonly ISchedulingService _schedulingService;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentDeleteRequestRepository _appointmentDeleteRequestRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly IPatientRepository _patientRepository;
        public ISchedulingService SchedulingService => _schedulingService;
        public IAppointmentRepository AppointmentRepository => _appointmentRepository;
        public IAppointmentDeleteRequestRepository AppointmentDeleteRequestRepository => _appointmentDeleteRequestRepository;
        public IActivityRepository ActivityRepository => _activityRepository;
        public IPatientRepository PatientRepository => _patientRepository;
        #endregion

        #region attributes
        private List<Appointment> _futureAppointments;
        private Appointment _selectedAppointment;
        #endregion

        #region properties
        public List<Appointment> FutureAppointments
        {
            get => _futureAppointments;
            set
            {
                _futureAppointments = value;
                OnPropertyChanged(nameof(FutureAppointments));
            }
        }

        public Appointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                _selectedAppointment = value;
                GlobalStore.AddObject("SelectedAppointment", value);
                OnPropertyChanged(nameof(SelectedAppointment));
            }
        }
        #endregion

        #region commands
        public ICommand? RecommendAppointmentCreationCommand { get; }
        public ICommand? AppointmentCreationCommand { get; }
        public ICommand? AppointmentUpdateCommand { get; }
        public ICommand? RemoveAppointmentCommand { get; }
        #endregion

        public PatientAppointmentsViewModel(ISchedulingService schedulingService, IAppointmentRepository appointmentRepository, IAppointmentDeleteRequestRepository appointmentDeleteRequestRepository, IActivityRepository activityRepository, IPatientRepository patientRepository)
        {
            _schedulingService = schedulingService;
            _appointmentDeleteRequestRepository = appointmentDeleteRequestRepository;
            _activityRepository = activityRepository;
            _patientRepository = patientRepository;
            _appointmentRepository = appointmentRepository;

            Patient pt = GlobalStore.ReadObject<Patient>("LoggedUser");
            FutureAppointments = SchedulingService.ReadFuturePatientAppointments(pt).OrderBy(apt => apt.StartDate).ToList();

            RecommendAppointmentCreationCommand = new RecommendAppointmentCreationCommand();
            AppointmentCreationCommand = new AppointmentCreationCommand();
            AppointmentUpdateCommand = new AppointmentUpdateCommand(this);
            RemoveAppointmentCommand = new RemoveAppointmentCommand(this);
        }
    }
}
