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
    public class MakeAppointmentCommand : CommandBase
    {
        private readonly AppointmentCreationViewModel? _viewModel;
        public MakeAppointmentCommand(AppointmentCreationViewModel viewModel) {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.Date) || e.PropertyName == nameof(_viewModel.Hours) || e.PropertyName == nameof(_viewModel.Minutes) || e.PropertyName == nameof(_viewModel.SelectedDoctor))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_viewModel.Date <= DateTime.Now) && !string.IsNullOrEmpty(_viewModel.Hours) && !string.IsNullOrEmpty(_viewModel.Minutes) && base.CanExecute(parameter) && !(_viewModel.SelectedDoctor == null);
        }

        public override void Execute(object? parameter) {
            var startTime = _viewModel.Date.Value.AddHours(Int32.Parse(_viewModel.Hours)).AddMinutes(Int32.Parse(_viewModel.Minutes));
            var doctorAppointments = _viewModel._appointmentService.ReadDoctorAppointemnts(_viewModel.SelectedDoctor, startTime, startTime.AddMinutes(15));

            MessageBox.Show("");
        }
    }
}
