using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Repository;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.RoomManagement
{
    public class RoomsCRUDViewModel : ViewModelBase
    {
        #region Attributes

        public readonly IRoomRepository _roomRepository;
        public readonly ISchedulingService _schedulingService;
        private List<Room> _rooms;
        private Room _selectedRoom;

        #endregion

        #region Properties

        public List<Room> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }
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

        #endregion

        #region Commands

        public ICommand? ViewRoomEquipmentCommand { get; }
        public ICommand? CreateRoomCommand { get; }
        public ICommand? OpenUpdateRoomCommand { get; }
        public ICommand? DeleteRoomCommand { get; }

        #endregion

        public RoomsCRUDViewModel(IRoomRepository roomRepository, ISchedulingService schedulingService)
        {
            _roomRepository = roomRepository;
            _schedulingService = schedulingService;

            _selectedRoom = null;
            _rooms = _roomRepository.ReadAll().ToList();
            _rooms = _rooms.OrderBy(x => x.Name).ToList();

            ViewRoomEquipmentCommand = new ViewRoomEquipmentCommand(this);
            CreateRoomCommand = new CreateRoomCommand();
            OpenUpdateRoomCommand = new OpenUpdateRoomCommand(this);
            DeleteRoomCommand = new DeleteRoomCommand(this, _schedulingService, _roomRepository);
        }
    }
}
