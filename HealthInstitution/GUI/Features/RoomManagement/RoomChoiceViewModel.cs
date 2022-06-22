using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Repository;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.RoomManagement
{
    public class RoomChoiceViewModel : ViewModelBase
    {
        #region Attributes

        public readonly IRoomRepository _roomRepository;
        private Room _selectedRoom1;
        private Room _selectedRoom2;
        private List<Room> _rooms;
        private DateTime _arrangeDateTime;

        #endregion

        #region Properties

        public Room SelectedRoom1
        {
            get => _selectedRoom1;
            set
            {
                _selectedRoom1 = value;
                OnPropertyChanged(nameof(SelectedRoom1));
            }
        }
        public Room SelectedRoom2
        {
            get => _selectedRoom2;
            set
            {
                _selectedRoom2 = value;
                OnPropertyChanged(nameof(SelectedRoom2));
            }
        }
        public List<Room> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }
        public DateTime ArrangeDateTime
        {
            get => _arrangeDateTime;
            set
            {
                _arrangeDateTime = value;
                OnPropertyChanged(nameof(ArrangeDateTime));
            }
        }

        #endregion

        #region Commands

        public ICommand? ConfirmRoomChoiceCommand { get; }

        #endregion

        public RoomChoiceViewModel(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
            _rooms = roomRepository.ReadAll().ToList();
            _rooms = _rooms.OrderBy(x => x.Name).ToList();
            _selectedRoom1 = _rooms[0];
            _selectedRoom2 = _rooms[1];
            _arrangeDateTime = DateTime.Now;
            ConfirmRoomChoiceCommand = new ConfirmRoomChoiceCommand(this);
        }
    }
}
