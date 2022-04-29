using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class RoomChoiceViewModel : ViewModelBase
    {
        public readonly IRoomService roomService;

        public ICommand? ConfirmRoomChoiceCommand { get; }

        private Room _selectedRoom1;
        public Room SelectedRoom1
        {
            get => _selectedRoom1;
            set
            {
                _selectedRoom1 = value;
                OnPropertyChanged(nameof(SelectedRoom1));
            }
        }

        private Room _selectedRoom2;
        public Room SelectedRoom2
        {
            get => _selectedRoom2;
            set
            {
                _selectedRoom2 = value;
                OnPropertyChanged(nameof(SelectedRoom2));
            }
        }

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

        public RoomChoiceViewModel(IRoomService roomService)
        {
            roomService = roomService;
            Rooms = roomService.ReadAll().ToList();
            SelectedRoom1 = Rooms[0];
            SelectedRoom2 = Rooms[1];
            ConfirmRoomChoiceCommand = new ConfirmRoomChoiceCommand(this);
        }
    }
}
