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
            else if (DateTime.Now.AddDays(2) > apt.StartDate) {
                Patient pt = GlobalStore.ReadObject<Patient>("LoggedUser");
                AppointmentRequest appointmentRequest = new AppointmentRequest(pt, apt, ActivityType.Delete);
                _viewModel._appointmentRequestService.Create(appointmentRequest);
                MessageBox.Show("Request for appointment deletion created successfully!\nPlease wait for secretary to review it.");

                Activity act = new Activity(pt, DateTime.Now, ActivityType.Delete);
                _viewModel._activityService.Create(act);
                
                var activityCount = _viewModel._activityService.ReadPatientUpdateOrRemoveActivity(pt, 30).ToList<Activity>().Count;
                if (activityCount >= 5)
                {
                    pt.IsBlocked = true;
                    MessageBox.Show("Your profile has been blocked!\n(Too many appointments removed or updated)");
                    EventBus.FireEvent("BackToLogin");
                }
            }
            else
            {
                Patient pt = GlobalStore.ReadObject<Patient>("LoggedUser");
                _viewModel._appointmentService.Delete(apt.Id);
                MessageBox.Show("Appointment deleted successfully!");

                Activity act = new Activity(pt, DateTime.Now, ActivityType.Delete);
                _viewModel._activityService.Create(act);
                
                var activityCount = _viewModel._activityService.ReadPatientUpdateOrRemoveActivity(pt, 30).ToList<Activity>().Count;
                if (activityCount >= 5)
                {
                    pt.IsBlocked = true;
                    MessageBox.Show("Your profile has been blocked!\n(Too many appointments removed or updated)");
                    EventBus.FireEvent("BackToLogin");
                }
                else
                {
                    EventBus.FireEvent("PatientAppointments");
                }

                /*
                _viewModel.Appointments.Remove(apt);
                _viewModel.SelectedAppointment = _viewModel.Appointments.First();
                */
            }
        }
    }
}
