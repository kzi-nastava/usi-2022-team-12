using System.ComponentModel;
using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.ViewModel.manager;

namespace HealthInstitution.Commands.manager
{
    public class ViewRoomEquipmentCommand : CommandBase
    {
        private readonly RoomsCRUDViewModel? _viewModel;

        public ViewRoomEquipmentCommand(RoomsCRUDViewModel viewModel)
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
            EventBus.FireEvent("RoomEquipment");
        }
    }
}
