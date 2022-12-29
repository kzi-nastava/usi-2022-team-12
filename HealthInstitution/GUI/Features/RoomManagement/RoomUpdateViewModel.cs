using System;
using System.Collections.Generic;
using System.Windows.Input;
using HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Repository;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.RoomManagement
{
    public class RoomUpdateViewModel : ViewModelBase
    {
        #region Attributes

        public readonly IRoomService _roomService;
        public readonly IRoomRepository _roomRepository;
        private string _roomName;
        private Room _selectedRoom;
        private RoomType _selectedType;
        private List<RoomType> _roomTypes;

        #endregion

        #region Properties

        public string? RoomName
        {
            get => _roomName;
            set
            {
                _roomName = value;
                OnPropertyChanged(nameof(RoomName));
            }
        }
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }
        public RoomType SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }
        public List<RoomType> RoomTypes
        {
            get => _roomTypes;
            set
            {
                _roomTypes = value;
                OnPropertyChanged(nameof(RoomTypes));
            }
        }

        #endregion

        #region Commands

        public ICommand? UpdateRoomCommand { get; }

        #endregion

        #region Methods
        private void LoadRoomTypes()
        {
            _roomTypes = new List<RoomType>();
            foreach (var type in Enum.GetValues(typeof(RoomType)))
            {
                _roomTypes.Add((RoomType)type);
            }
        }

        #endregion

        public RoomUpdateViewModel(IRoomService roomService, IRoomRepository roomRepository)
        {
            _roomService = roomService;
            _roomRepository = roomRepository;
            _selectedRoom = GlobalStore.ReadObject<Room>("SelectedRoom");
            _selectedType = _selectedRoom.RoomType;
            _roomName = _selectedRoom.Name;

            LoadRoomTypes();

            UpdateRoomCommand = new UpdateRoomCommand(this, SelectedRoom);
        }
    }
}
