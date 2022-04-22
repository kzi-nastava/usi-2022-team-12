using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
{
    public class AppointmentCreationCommand : CommandBase
    {
        private readonly AppointmentCreationViewModel? _viewModel;

        public AppointmentCreationCommand(AppointmentCreationViewModel acvm)
        {
            _viewModel = acvm;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.Date) || e.PropertyName == nameof(_viewModel.Time) || e.PropertyName == nameof(_viewModel.SelectedDoctor))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_viewModel.Date < DateTime.Now) && !string.IsNullOrEmpty(_viewModel.Time) && base.CanExecute(parameter) && !(_viewModel.SelectedDoctor == null); 
        }

        public override void Execute(object? parameter)
        {
            
        }
    }
}
