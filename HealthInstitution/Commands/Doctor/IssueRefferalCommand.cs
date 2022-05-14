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
