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

        public PatientAppointmentsViewModel(IAppointmentService appointmentService)
        {
            AppointmentCreationCommand = new AppointmentCreationCommand();
            _appointmentService = appointmentService;
            Appointments = _appointmentService.ReadPatientAppointments(GlobalStore.ReadObject<Patient>("LoggedUser")).ToList();
        }
    }
}
