using System;
using System.Collections.Generic;
using System.Windows.Input;
using HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Repository;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.RoomManagement
{
    public class RoomCreateViewModel : ViewModelBase
    {
        #region Attributes

        public readonly IRoomService _roomService;
        public readonly IRoomRenovationRepository _roomRenovationRepository;
        public readonly IRoomRepository _roomRepository;
        private string _roomName;
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

        public ICommand? AddRoomCommand { get; }

        #endregion

        #region Methods

        private void LoadRoomTypes()
        {
            _roomTypes = new List<RoomType>();
            foreach (var type in Enum.GetValues(typeof(RoomType)))
            {
                _roomTypes.Add((RoomType)type);
            }
            _selectedType = _roomTypes[0];
        }

        #endregion

        public RoomCreateViewModel(IRoomService roomService, IRoomRenovationRepository roomRenovationRepository, IRoomRepository roomRepository)
        {
            _roomService = roomService;
            _roomRenovationRepository = roomRenovationRepository;
            _roomRepository = roomRepository;

            LoadRoomTypes();

            AddRoomCommand = new AddRoomCommand(this);
        }
    }
}
