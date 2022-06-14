using System;
using System.ComponentModel;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.ViewModel.patient;

namespace HealthInstitution.Commands.patient
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
            return !(_viewModel.DeadlineDate.AddMinutes(_viewModel.StartTime.TimeOfDay.TotalMinutes) <= DateTime.Now) && !(_viewModel.SelectedDoctor == null) && 
                !(_viewModel.SelectedPriority == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            Patient patient = GlobalStore.ReadObject<Patient>("LoggedUser");
            TimeOnly startTime = TimeOnly.FromDateTime(_viewModel.StartTime);
            TimeOnly endTime = TimeOnly.FromDateTime(_viewModel.EndTime);
            DateOnly deadline = DateOnly.FromDateTime(_viewModel.DeadlineDate);
            _viewModel.RecommendedAppointments = _viewModel.AppointmentService.RecommendAppointments(patient, _viewModel.SelectedDoctor, startTime, endTime, deadline, _viewModel.SelectedPriority);
        }
    }
}
