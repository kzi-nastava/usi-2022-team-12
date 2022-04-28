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
            EventBus.FireEvent("Examination");
        }
    }
}
