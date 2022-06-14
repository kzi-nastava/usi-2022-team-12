using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.EquipmentManagement;

namespace HealthInstitution.Core.Features.EquipmentManagement.Commands.ManagerCMD
{
    public class SecondToFirstCommand : CommandBase
    {
        private readonly ArrangeEquipmentViewModel? _viewModel;

        public SecondToFirstCommand(ArrangeEquipmentViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.MoveItemBetweenRooms(_viewModel.SelectedEntry2,
                (System.Collections.ObjectModel.ObservableCollection<Entry<Equipment>>)_viewModel.Inventory2,
                (System.Collections.ObjectModel.ObservableCollection<Entry<Equipment>>)_viewModel.Inventory1);
        }
    }
}
