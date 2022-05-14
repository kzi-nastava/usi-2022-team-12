using HealthInstitution.Model;
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
    public class RecommendAppointmentCommand : CommandBase
    {
        private readonly RecommendAppointmentCreationViewModel? _viewModel;
        public RecommendAppointmentCommand(RecommendAppointmentCreationViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.DeadlineDate) || e.PropertyName == nameof(_viewModel.StartTime) || e.PropertyName == nameof(_viewModel.EndTime) 
                || e.PropertyName == nameof(_viewModel.SelectedDoctor) || e.PropertyName == nameof(_viewModel.SelectedPriority))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_viewModel.DeadlineDate.AddSeconds(_viewModel.StartTime.TimeOfDay.TotalSeconds) <= DateTime.Now) && !(_viewModel.SelectedDoctor == null) && 
                !(_viewModel.SelectedPriority == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            Patient patient = GlobalStore.ReadObject<Patient>("LoggedUser");
            _viewModel.RecommendedAppointments = _viewModel.AppointmentService.RecommendAppointments(patient, _viewModel.SelectedDoctor,
                _viewModel.StartTime, _viewModel.EndTime, _viewModel.DeadlineDate, _viewModel.SelectedPriority);
        }
    }
}
