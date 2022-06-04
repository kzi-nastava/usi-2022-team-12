using HealthInstitution.Model.room;
using HealthInstitution.ViewModel.manager;

namespace HealthInstitution.Commands.manager
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
                (System.Collections.ObjectModel.ObservableCollection<Model.Entry<Equipment>>)_viewModel.Inventory1, 
                (System.Collections.ObjectModel.ObservableCollection<Model.Entry<Equipment>>)_viewModel.Inventory2);
        }
    }
}
