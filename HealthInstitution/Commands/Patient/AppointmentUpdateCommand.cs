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
            if (DateTime.Now.AddDays(1) > GlobalStore.ReadObject<Appointment>("ChosenAppointment").StartDate)
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
