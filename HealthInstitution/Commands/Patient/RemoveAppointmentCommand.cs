using HealthInstitution.Model;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            if (DateTime.Now.AddDays(1) > apt.StartDate)
            {
                MessageBox.Show("You can't remove appointment in last 24h!");
            }
            else 
            {
                _viewModel._appointmentService.Delete(apt.Id);
                Activity act = new Activity(DateTime.Now, ActivityType.Delete);
                Patient pt = GlobalStore.ReadObject<Patient>("LoggedUser");
                pt.AddActivity(act);
                MessageBox.Show("Appointment deleted Successfully");
                EventBus.FireEvent("PatientAppointments");
                /*
                _viewModel.Appointments.Remove(apt);
                _viewModel.SelectedAppointment = _viewModel.Appointments.First();
                */
            }
        }
    }
}
