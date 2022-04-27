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
using System.Windows;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class RoomUpdateViewModel : ViewModelBase
    {
        public readonly IRoomService _roomService;

        public ICommand? UpdateRoomCommand { get; }

        private string _roomName;
        public string? RoomName
        {
            get => _roomName;
            set
            {
                _roomName = value;
                OnPropertyChanged(nameof(RoomName));
            }
        }

        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

        private RoomType _selectedType;
        public RoomType SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }

        private List<RoomType> _roomTypes;
        public List<RoomType> RoomTypes
        {
            get => _roomTypes;
            set
            {
                _roomTypes = value;
                OnPropertyChanged(nameof(RoomTypes));
            }
        }

        public RoomUpdateViewModel(IRoomService roomService)
        {
            _roomService = roomService;
            SelectedRoom = GlobalStore.ReadObject<Room>("SelectedRoom");
            SelectedType = SelectedRoom.RoomType;
            RoomName = SelectedRoom.Name;

            RoomTypes = new List<RoomType>();
            RoomTypes.Add(RoomType.ExaminationRoom);
            RoomTypes.Add(RoomType.OperationRoom);
            RoomTypes.Add(RoomType.RestingRoom);
            if (SelectedType.Equals(RoomType.Storage))
            {
                RoomTypes.Add(RoomType.Storage);
            }
            UpdateRoomCommand = new UpdateRoomCommand(this, SelectedRoom);
        }
    }
}
