using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.AppointmentScheduling;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Commands.PatientCMD
{
    public class ConfirmRecommendationCommand : CommandBase
    {
        private readonly RecommendAppointmentCreationViewModel? _viewModel;
        public ConfirmRecommendationCommand(RecommendAppointmentCreationViewModel viewModel)
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
            _viewModel.AppointmentService.Create(_viewModel.SelectedAppointment);
            Patient pt = GlobalStore.ReadObject<Patient>("LoggedUser");
            Activity act = new Activity(pt, DateTime.Now, ActivityType.Create);
            _viewModel.ActivityService.Create(act);
            MessageBox.Show("Appointment created successfully!");
            var activityCount = _viewModel.ActivityService.ReadPatientMakeActivity(pt, 30).ToList<Activity>().Count;
            if (activityCount > 8)
            {
                pt.IsBlocked = true;
                _viewModel.PatientService.Update(pt);
                MessageBox.Show("Your profile has been blocked!\n(Too many appointments made)");
                EventBus.FireEvent("BackToLogin");
            }
            else
            {
                EventBus.FireEvent("PatientAppointments");
            }
        }
    }
}
