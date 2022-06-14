using System.ComponentModel;
using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.ViewModel.doctor;

namespace HealthInstitution.Core.Features.MedicalRecordManagement.Commands.DoctorCMD
{
    public class AddIllnessCommand : CommandBase
    {
        private ExaminationViewModel _viewModel;

        public AddIllnessCommand(ExaminationViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            Illness illness = new() { Name = _viewModel.NewIllnessName };
            _viewModel.AddIllness(illness);
            _viewModel.NewIllnessName = "";
        }
        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.NewIllnessName);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.NewIllnessName))
            {
                OnCanExecuteChange();
            }
        }
    }
}
