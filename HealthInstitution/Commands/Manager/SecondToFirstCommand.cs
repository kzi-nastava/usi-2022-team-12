using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
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
                (System.Collections.ObjectModel.ObservableCollection<Model.Entry<Model.Equipment>>)_viewModel.Inventory2,
                (System.Collections.ObjectModel.ObservableCollection<Model.Entry<Model.Equipment>>)_viewModel.Inventory1);
        }
    }
}
