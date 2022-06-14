using System.ComponentModel;
using System.Windows;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.ViewModel.manager;

namespace HealthInstitution.Commands.manager
{
    public class UpdateIngredientCommand : CommandBase
    {

        private readonly IngredientOverviewViewModel? _viewModel;
        public UpdateIngredientCommand(IngredientOverviewViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.NameBox) || e.PropertyName == nameof(_viewModel.SelectedIngredient))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.NameBox) && _viewModel.SelectedIngredient != null && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            bool ingredientExists = _viewModel.IngredientService.IngredientExists(_viewModel.NameBox);
            if (ingredientExists)
            {
                MessageBox.Show("Ingredient with that name already exists!");
                return;
            }
            Ingredient i = _viewModel.SelectedIngredient;
            i.Name = _viewModel.NameBox;
            _viewModel.IngredientService.Update(i);
            MessageBox.Show("Ingredient updated successfully!");
            EventBus.FireEvent("IngredientOverview");
        }
    }
}