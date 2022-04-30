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
    public class NavigateDoctorAppointmentUpdateCommand : CommandBase
    {
        private DoctorScheduleViewModel _viewModel;
        public NavigateDoctorAppointmentUpdateCommand(DoctorScheduleViewModel viewModel)
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
            return _viewModel.SelectedAppointment is not null && base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            Appointment apt = _viewModel.SelectedAppointment.Appointment;
            if(DateTime.Now > apt.StartDate)
            {
                MessageBox.Show("You can't update past appointment!");
            }
            else if(DateTime.Now.AddDays(1)> apt.StartDate)
            {
                MessageBox.Show("You can't update appointment in last 24h!");
            }
            else
            {
                GlobalStore.AddObject("SelectedAppointment", _viewModel.SelectedAppointment.Appointment);
                EventBus.FireEvent("UpdateAppointment");
            }
        }
    }
}
