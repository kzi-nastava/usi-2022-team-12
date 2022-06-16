using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.EquipmentManagement;

namespace HealthInstitution.Core.Features.EquipmentManagement.Commands.ManagerCMD
{
    public class FirstToSecondCommand : CommandBase
    {
        private readonly ArrangeEquipmentViewModel? _viewModel;

        public FirstToSecondCommand(ArrangeEquipmentViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.MoveItemBetweenRooms(_viewModel.SelectedEntry1, 
                (System.Collections.ObjectModel.ObservableCollection<Entry<Equipment>>)_viewModel.Inventory1, 
                (System.Collections.ObjectModel.ObservableCollection<Entry<Equipment>>)_viewModel.Inventory2);
        }
    }
}
