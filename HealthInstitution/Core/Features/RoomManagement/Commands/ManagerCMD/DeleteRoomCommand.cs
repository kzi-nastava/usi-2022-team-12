using System.ComponentModel;
using System.Linq;
using System.Windows;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Services.Interfaces;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.RoomManagement;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD
{
    public class DeleteRoomCommand : CommandBase
    {
        private readonly RoomsCRUDViewModel? _viewModel;
        private Room _selectedRoom;
        private IAppointmentService _appointmentService;
        private IRoomService _roomService;

        public DeleteRoomCommand(RoomsCRUDViewModel viewModel, IAppointmentService appointmentService, IRoomService roomService)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
            _appointmentService = appointmentService;
            _roomService = roomService;
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
            _selectedRoom = GlobalStore.ReadObject<Room>("SelectedRoom");
            var apts = _appointmentService.ReadRoomAppointments(_selectedRoom);
            

            if (_selectedRoom.Inventory.Count() != 0)
            {
                MessageBox.Show("Room with that name has equipment!");
            }
            else if (apts.Count() != 0)
            {
                MessageBox.Show("Room with that name has appointments!");
            }
            else 
            {
                _roomService.Delete(_selectedRoom.Id);
                MessageBox.Show("Room deleted successfully!");
                EventBus.FireEvent("RoomsOverview");
            }

        }
    }
}