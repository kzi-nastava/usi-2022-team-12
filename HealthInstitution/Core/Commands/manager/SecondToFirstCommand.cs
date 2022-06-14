using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.ViewModel.manager;

namespace HealthInstitution.Commands.manager
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
                (System.Collections.ObjectModel.ObservableCollection<Model.Entry<Equipment>>)_viewModel.Inventory2,
                (System.Collections.ObjectModel.ObservableCollection<Model.Entry<Equipment>>)_viewModel.Inventory1);
        }
    }
}
