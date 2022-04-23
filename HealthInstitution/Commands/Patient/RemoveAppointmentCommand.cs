using HealthInstitution.Model;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
{
    public class RemoveAppointmentCommand : CommandBase
    {
        private readonly PatientAppointmentsViewModel? _viewModel;
        public RemoveAppointmentCommand(PatientAppointmentsViewModel vm) {
            _viewModel = vm;
        }
        public override void Execute(object? parameter)
        {
            var apt = GlobalStore.ReadObject<Appointment>("ChosenAppointment");
            _viewModel._appointmentService.Delete(apt.Id);
            EventBus.FireEvent("PatientAppointments");
            /*
            _viewModel.Appointments.Remove(apt);
            _viewModel.SelectedAppointment = _viewModel.Appointments.First();
            */
        }
    }
}
