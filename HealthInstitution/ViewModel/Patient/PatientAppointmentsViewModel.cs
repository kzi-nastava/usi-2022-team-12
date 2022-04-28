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
        public readonly IAppointmentService _appointmentService;
        public readonly IAppointmentDeleteRequestService _appointmentDeleteRequestService;
        public readonly IActivityService _activityService;
        public readonly IPatientService _patientService;
        public ICommand? AppointmentCreationCommand { get; }
        public ICommand? AppointmentUpdateCommand { get; }
        public ICommand? RemoveAppointmentCommand { get; }

        private List<Appointment> _appointments;
        public List<Appointment> Appointments
        {
            get => _appointments;
            set
            {
                _appointments = value;
                OnPropertyChanged(nameof(Appointments));
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

        public PatientAppointmentsViewModel(IAppointmentService appointmentService, IAppointmentDeleteRequestService appointmentDeleteRequestService, IActivityService activityService, IPatientService patientService)
        {
            _appointmentService = appointmentService;
            _appointmentDeleteRequestService = appointmentDeleteRequestService;
            _activityService = activityService;
            _patientService = patientService;
            Appointments = _appointmentService.ReadPatientAppointments(GlobalStore.ReadObject<Patient>("LoggedUser")).ToList();
            AppointmentCreationCommand = new AppointmentCreationCommand();
            AppointmentUpdateCommand = new AppointmentUpdateCommand(this);
            RemoveAppointmentCommand = new RemoveAppointmentCommand(this);
        }
    }
}
