using System.ComponentModel;
using System.Linq;
using System.Windows;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Repository;
using HealthInstitution.Core.Ninject;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.RoomManagement;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD
{
    public class UpdateRoomCommand : CommandBase
    {

        private string _roomName;
        private RoomType _roomType;

        private readonly RoomUpdateViewModel? _viewModel;
        public UpdateRoomCommand(RoomUpdateViewModel viewModel, Room selectedRoom)
        {
            _viewModel = viewModel;
            _roomName = selectedRoom.Name;
            _roomType = selectedRoom.RoomType;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.RoomName) || e.PropertyName == nameof(_viewModel.SelectedType))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.RoomName) && (!_viewModel.RoomName.Equals(_roomName) ||
               !_viewModel.SelectedType.Equals(_roomType)) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            string roomName = _viewModel.RoomName;
            RoomType roomType = _viewModel.SelectedType;

            var rooms = _viewModel._roomService.ReadRoomsWithName(roomName);

            if (rooms.Count() == 0 || (!rooms.ElementAt(0).RoomType.Equals(roomType) && roomName.Equals(_roomName)))
            {
                _viewModel.SelectedRoom.Name = roomName;
                _viewModel.SelectedRoom.RoomType = roomType;
                ServiceLocator.Get<RoomRepository>().Update(_viewModel.SelectedRoom);
                MessageBox.Show("Room updated successfully!");
                EventBus.FireEvent("RoomsOverview");
            }
            else
            {
                MessageBox.Show("Room with that name already exists!");
                return;
            }
        }
    }
}