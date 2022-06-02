using System.ComponentModel;
using System.Linq;
using HealthInstitution.Dialogs.Custom.Doctor;
using HealthInstitution.Model;
using HealthInstitution.Model.room;

namespace HealthInstitution.Commands.doctor
{
    public class MoveToUsedCommand : CommandBase
    {
        private DynamicEquipmentUpdateViewModel _viewModel;
        public MoveToUsedCommand(DynamicEquipmentUpdateViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SelectedItemInventory))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedItemInventory is not null
                && _viewModel.SelectedItemInventory.Quantity > 0
                && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _viewModel.SelectedItemInventory.Quantity--;
            Entry<Equipment>? item = _viewModel.UsedEquipment.FirstOrDefault(e => e.Id == _viewModel.SelectedItemInventory.Id);
            if (item is null)
            {
                var entry = new Entry<Equipment>(_viewModel.SelectedItemInventory);
                entry.Quantity = 1;
                _viewModel.UsedEquipment.Add(entry);
            }
            else
            {
                item.Quantity++;
            }
            _viewModel.Inventory.ResetBindings();
        }
    }
}
