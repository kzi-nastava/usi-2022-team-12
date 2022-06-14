using System.ComponentModel;
using HealthInstitution.Core.Features.OperationsAndExaminations.Model;
using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.ViewModel.doctor;

namespace HealthInstitution.Commands.doctor
{
    public class IssueRefferalCommand : CommandBase
    {
        DoctorReferralCreationViewModel _viewModel;
        public IssueRefferalCommand(DoctorReferralCreationViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SelectedDoctor))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedDoctor is not null
                && base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            Referral referral = new Referral { Doctor = _viewModel.SelectedDoctor, IsUsed = false, Patient = _viewModel.Patient };
            _viewModel.addReferral(referral);
        }
    }
}
