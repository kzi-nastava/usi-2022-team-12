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
        public List<Appointment> FutureAppointments
        {
            get => _futureAppointments;
            set
            {
                _futureAppointments = value;
                OnPropertyChanged(nameof(FutureAppointments));
            }
        }

        private Appointment _selectedAppointment;
        public Appointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                _selectedAppointment = value;
                GlobalStore.AddObject("ChosenAppointment", value);
                OnPropertyChanged(nameof(SelectedAppointment));
            }
        }
        #endregion

        #region commands
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
            FutureAppointments = AppointmentService.ReadFuturePatientAppointments(pt).OrderByDescending(apt => apt.StartDate).ToList();
            AppointmentCreationCommand = new AppointmentCreationCommand();
            AppointmentUpdateCommand = new AppointmentUpdateCommand(this);
            RemoveAppointmentCommand = new RemoveAppointmentCommand(this);
        }
    }
}
