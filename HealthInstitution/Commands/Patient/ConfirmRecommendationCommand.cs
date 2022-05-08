using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
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
            
        }
    }
}
