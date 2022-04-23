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

        public PatientAppointmentsViewModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
            Appointments = _appointmentService.ReadPatientAppointments(GlobalStore.ReadObject<Patient>("LoggedUser")).ToList();
            SelectedAppointment = Appointments.First();
            AppointmentCreationCommand = new AppointmentCreationCommand();
            AppointmentUpdateCommand = new AppointmentUpdateCommand();
            RemoveAppointmentCommand = new RemoveAppointmentCommand(this);
        }
    }
}
