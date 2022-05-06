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
        public RemoveAppointmentCommand(PatientAppointmentsViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SelectedAppointment))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_viewModel.SelectedAppointment == null) && base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            var apt = GlobalStore.ReadObject<Appointment>("ChosenAppointment");
            if (DateTime.Now > apt.StartDate)
            {
                MessageBox.Show("You can't remove expired appointment!");
            }

            else if (DateTime.Now.AddDays(1) > apt.StartDate)
            {
                MessageBox.Show("You can't remove appointment in last 24h!");
            }

            else if (DateTime.Now.AddDays(2) > apt.StartDate)
            {
                Patient pt = GlobalStore.ReadObject<Patient>("LoggedUser");
                AppointmentDeleteRequest appointmentRequest = new AppointmentDeleteRequest { Patient = pt, Appointment = apt, ActivityType = ActivityType.Delete, Status = Status.Pending };
                _viewModel.AppointmentDeleteRequestService.Create(appointmentRequest);
                MessageBox.Show("Request for appointment deletion created successfully!\nPlease wait for secretary to review it.");

                Activity act = new Activity(pt, DateTime.Now, ActivityType.Delete);
                _viewModel.ActivityService.Create(act);

                var activityCount = _viewModel.ActivityService.ReadPatientUpdateOrRemoveActivity(pt, 30).ToList<Activity>().Count;
                if (activityCount >= 5)
                {
                    pt.IsBlocked = true;
                    _viewModel.PatientService.Update(pt);
                    MessageBox.Show("Your profile has been blocked!\n(Too many appointments removed or updated)");
                    EventBus.FireEvent("BackToLogin");
                }
            }

            else
            {
                Patient pt = GlobalStore.ReadObject<Patient>("LoggedUser");
                _viewModel.AppointmentService.Delete(apt.Id);
                MessageBox.Show("Appointment deleted successfully!");

                Activity act = new Activity(pt, DateTime.Now, ActivityType.Delete);
                _viewModel.ActivityService.Create(act);

                var activityCount = _viewModel.ActivityService.ReadPatientUpdateOrRemoveActivity(pt, 30).ToList<Activity>().Count;
                if (activityCount >= 5)
                {
                    pt.IsBlocked = true;
                    _viewModel.PatientService.Update(pt);
                    MessageBox.Show("Your profile has been blocked!\n(Too many appointments removed or updated)");
                    EventBus.FireEvent("BackToLogin");
                }
                else
                {
                    _viewModel.FutureAppointments = _viewModel.AppointmentService.ReadFuturePatientAppointments(pt).OrderBy(apt => apt.StartDate).ToList();
                }
            }
        }
    }
}
