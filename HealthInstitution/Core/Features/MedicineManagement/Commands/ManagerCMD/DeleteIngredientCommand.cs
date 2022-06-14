using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.ViewModel.manager;

namespace HealthInstitution.Commands.manager
{
    public class DeleteIngredientCommand : CommandBase
    {

        private readonly IngredientOverviewViewModel? _viewModel;
        public DeleteIngredientCommand(IngredientOverviewViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SelectedIngredient))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedIngredient != null && base.CanExecute(parameter);
        }

        private bool CanDelete(Ingredient i)
        {
            List<Medicine> approvedMed = _viewModel.MedicineService.GetApprovedMedicine().ToList();
            foreach (var med in approvedMed)
            {
                foreach (var ing in med.Ingredients)
                {
                    if (ing.Id == i.Id)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override void Execute(object? parameter)
        {
            bool canDelete = CanDelete(_viewModel.SelectedIngredient);
            if (canDelete)
            {
                _viewModel.IngredientService.Delete(_viewModel.SelectedIngredient.Id);
                MessageBox.Show("Ingredient deleted successfully!");
                EventBus.FireEvent("IngredientOverview");
                return;
            }
            MessageBox.Show("Failed to delete! The ingredient is contained in a medicine.");
        }
    }
}