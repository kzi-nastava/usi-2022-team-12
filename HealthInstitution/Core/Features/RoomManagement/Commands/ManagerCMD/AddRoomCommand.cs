using System.ComponentModel;
using System.Linq;
using System.Windows;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.RoomManagement;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD
{
    public class AddRoomCommand : CommandBase
    {

        private readonly RoomCreateViewModel? _viewModel;
        public AddRoomCommand(RoomCreateViewModel viewModel)
        {
            _viewModel = viewModel;
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
            return !string.IsNullOrEmpty(_viewModel.RoomName) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            string roomName = _viewModel.RoomName;
            var rooms = _viewModel._roomService.ReadRoomsWithName(roomName);
            var renRooms = _viewModel._roomRenovationService.ReadAll();

            foreach (var renRoom in renRooms)
            {
                if (renRoom.AdvancedDivide != null)
                {
                    if (renRoom.RenovatedSmallRoom1Name.Equals(roomName) || renRoom.RenovatedSmallRoom2Name.Equals(roomName))
                    {
                        MessageBox.Show("Room with that name already exists! Currently in renovation.");
                        return;
                    }
                }
            }
            if (rooms.Count() != 0)
            {
                MessageBox.Show("Room with that name already exists!");
                return;  
            }

            Room r = new Room(_viewModel.SelectedType, roomName);
            _viewModel._roomService.Create(r);
            MessageBox.Show("Room created successfully!");
            EventBus.FireEvent("RoomsOverview");

            EventBus.FireEvent("AddRoom");
        }
    }
}