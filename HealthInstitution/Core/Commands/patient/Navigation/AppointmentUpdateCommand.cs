using System;
using System.ComponentModel;
using System.Windows;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.ViewModel.patient;

namespace HealthInstitution.Commands.patient.Navigation
{
    public class AppointmentUpdateCommand : CommandBase
    {
        private readonly PatientAppointmentsViewModel? _viewModel;
        public AppointmentUpdateCommand(PatientAppointmentsViewModel viewModel)
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
            Appointment apt = GlobalStore.ReadObject<Appointment>("SelectedAppointment");
            if (DateTime.Now > apt.StartDate)
            {
                MessageBox.Show("You can't update expired appointment!");
            }
            else if (DateTime.Now.AddDays(1) > apt.StartDate)
            {
                MessageBox.Show("You can't update appointment in last 24h!");
            }
            else
            {
                EventBus.FireEvent("AppointmentUpdate");
            }
        }
    }
}
