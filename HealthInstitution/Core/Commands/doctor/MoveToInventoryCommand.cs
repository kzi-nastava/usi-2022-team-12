using System.ComponentModel;
using System.Linq;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.Dialogs.Custom.Doctor;
using HealthInstitution.Model;

namespace HealthInstitution.Commands.doctor
{
    public class MoveToInventoryCommand : CommandBase
    {
        private DynamicEquipmentUpdateViewModel _viewModel;
        public MoveToInventoryCommand(DynamicEquipmentUpdateViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SelectedItemUsed))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedItemUsed is not null
                && _viewModel.SelectedItemUsed.Quantity > 0
                && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            Entry<Equipment> item = _viewModel.Inventory.First(e => e.Id == _viewModel.SelectedItemUsed.Id);                       
            if(_viewModel.SelectedItemUsed.Quantity == 1)
            {
                _viewModel.UsedEquipment.Remove(_viewModel.SelectedItemUsed);
            }
            else
            {
                _viewModel.SelectedItemUsed.Quantity--;
            }
            item.Quantity++;
            _viewModel.Inventory.ResetBindings();
        }
    }
}
