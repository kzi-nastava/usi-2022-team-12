using System;
using System.ComponentModel;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.AppointmentScheduling;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.Core.Features.OperationsAndExaminations.Commands.DoctorCMD
{
    public class StartAppointmentCommand : CommandBase
    {
        private DoctorScheduleViewModel _viewModel;
        public StartAppointmentCommand(ViewModelBase viewModel)
        {
            _viewModel = (DoctorScheduleViewModel)viewModel;
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
            return _viewModel.SelectedAppointment is not null
                && !_viewModel.SelectedAppointment.IsDone
                && _viewModel.SelectedAppointment.Appointment.StartDate.Date >= DateTime.Now.Date
                && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            GlobalStore.AddObject("SelectedAppointment", _viewModel.SelectedAppointment.Appointment);
            GlobalStore.AddObject("SelectedPatient", _viewModel.SelectedAppointment.Appointment.Patient);
            EventBus.FireEvent("Examination");
        }
    }
}
