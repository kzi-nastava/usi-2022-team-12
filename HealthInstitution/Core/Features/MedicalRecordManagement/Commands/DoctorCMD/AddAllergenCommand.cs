using System.ComponentModel;
using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.OperationsAndExaminations;

namespace HealthInstitution.Core.Features.MedicalRecordManagement.Commands.DoctorCMD
{
    public class AddAllergenCommand : CommandBase
    {
        private readonly ExaminationViewModel _viewModel;

        public AddAllergenCommand(ExaminationViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            Allergen allergen = new() { Name = _viewModel.NewAllergenName };
            _viewModel.AddAllergens(allergen);
            _viewModel.NewAllergenName = "";
        }
        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.NewAllergenName) && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.NewAllergenName))
            {
                OnCanExecuteChange();
            }
        }
    }
}
