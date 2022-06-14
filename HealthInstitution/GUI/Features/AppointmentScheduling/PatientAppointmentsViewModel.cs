using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Core.Features.AppointmentScheduling.Commands.PatientCMD;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Services.Interfaces;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.AppointmentScheduling
{
    public class PatientAppointmentsViewModel : ViewModelBase
    {
        #region services
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentDeleteRequestService _appointmentDeleteRequestService;
        private readonly IActivityService _activityService;
        private readonly IPatientService _patientService;
        public IAppointmentService AppointmentService => _appointmentService;
        public IAppointmentDeleteRequestService AppointmentDeleteRequestService => _appointmentDeleteRequestService;
        public IActivityService ActivityService => _activityService;
        public IPatientService PatientService => _patientService;
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

        public PatientAppointmentsViewModel(IAppointmentService appointmentService, IAppointmentDeleteRequestService appointmentDeleteRequestService, IActivityService activityService, IPatientService patientService)
        {
            _appointmentService = appointmentService;
            _appointmentDeleteRequestService = appointmentDeleteRequestService;
            _activityService = activityService;
            _patientService = patientService;

            Patient pt = GlobalStore.ReadObject<Patient>("LoggedUser");
            FutureAppointments = AppointmentService.ReadFuturePatientAppointments(pt).OrderBy(apt => apt.StartDate).ToList();

            RecommendAppointmentCreationCommand = new RecommendAppointmentCreationCommand();
            AppointmentCreationCommand = new AppointmentCreationCommand();
            AppointmentUpdateCommand = new AppointmentUpdateCommand(this);
            RemoveAppointmentCommand = new RemoveAppointmentCommand(this);
        }
    }
}
