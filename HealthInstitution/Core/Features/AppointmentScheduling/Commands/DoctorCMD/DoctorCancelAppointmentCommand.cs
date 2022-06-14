using System;
using System.ComponentModel;
using System.Windows;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.ViewModel.doctor;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Commands.DoctorCMD
{
    public class DoctorCancelAppointmentCommand : CommandBase
    {
        private DoctorScheduleViewModel _viewModel;
        public DoctorCancelAppointmentCommand(DoctorScheduleViewModel viewModel)
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
            return _viewModel.SelectedAppointment is not null && !_viewModel.SelectedAppointment.IsDone && base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            Appointment apt = _viewModel.SelectedAppointment.Appointment;
            if (DateTime.Now > apt.StartDate)
            {
                MessageBox.Show("You can't cancel past appointment!");
                return;
            }
            else
            {
                _viewModel.AppointmentService.Delete(apt.Id);
            }
            GlobalStore.AddObject("SelectedAppointment", _viewModel.SelectedAppointment.Appointment);
            _viewModel.UpdateData();
            EventBus.FireEvent("DoctorSchedule");
        }
    }
}
