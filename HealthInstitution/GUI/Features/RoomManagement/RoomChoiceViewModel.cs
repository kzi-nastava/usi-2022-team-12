using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Commands.manager;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.GUI.Utility.ViewModel;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.ViewModel.manager
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

        private DateTime _arrangeDateTime;
        public DateTime ArrangeDateTime
        {
            get => _arrangeDateTime;
            set
            {
                _arrangeDateTime = value;
                OnPropertyChanged(nameof(ArrangeDateTime));
            }
        }

        public RoomChoiceViewModel(IRoomService roomService)
        {

            roomService = roomService;
            Rooms = roomService.ReadAll().ToList();
            Rooms = Rooms.OrderBy(x => x.Name).ToList();
            SelectedRoom1 = Rooms[0];
            SelectedRoom2 = Rooms[1];
            ArrangeDateTime = DateTime.Now;
            ConfirmRoomChoiceCommand = new ConfirmRoomChoiceCommand(this);
        }
    }
}
