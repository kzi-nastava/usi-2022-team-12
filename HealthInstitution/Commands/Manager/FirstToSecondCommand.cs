using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
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
                (System.Collections.ObjectModel.ObservableCollection<Model.Entry<Model.Equipment>>)_viewModel.Inventory1, 
                (System.Collections.ObjectModel.ObservableCollection<Model.Entry<Model.Equipment>>)_viewModel.Inventory2);
        }
    }
}
