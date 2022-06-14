using System.ComponentModel;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.RoomManagement;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD
{
    public class OpenUpdateRoomCommand : CommandBase
    {
        private readonly RoomsCRUDViewModel? _viewModel;

        public OpenUpdateRoomCommand(RoomsCRUDViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;

        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SelectedRoom))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedRoom != null;
        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("OpenUpdateRoom");
        }
    }
}
