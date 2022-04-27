using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class RoomsCRUDViewModel : ViewModelBase
    {
        public readonly IRoomService _roomService;

        public ICommand? ViewRoomEquipmentCommand { get; }

        public ICommand? CreateRoomCommand { get; }

        private List<Room> _rooms;

        public List<Room> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }

        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                GlobalStore.AddObject("SelectedRoom", value);
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

        public RoomsCRUDViewModel(IRoomService roomService)
        {
            ViewRoomEquipmentCommand = new ViewRoomEquipmentCommand();
            CreateRoomCommand = new CreateRoomCommand();
            _roomService = roomService;
            Rooms = _roomService.ReadAll().ToList();
            
        }
    }
}
