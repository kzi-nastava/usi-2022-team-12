using System;
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
    public class RoomRenovationViewModel : ViewModelBase
    {
        #region Attributes

        public readonly IRoomRepository _roomRepository;
        public readonly ISchedulingService _schedulingService;
        public readonly IRoomService _roomService;
        public readonly IRoomRenovationRepository _roomRenovationRepository;
        private List<Room> _rooms;
        private string _bigRoomDivisionName;
        private string _roomDivision1;
        private string _roomDivision2;
        private List<Room> _rooms1;
        private List<Room> _rooms2;
        private Room _selectedRoom;
        private DateTime _startDate;
        private DateTime _endDate;
        private Room _selectedRoomMerge1;
        private Room _selectedRoomMerge2;

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
        public string? BigRoomDivisionName
        {
            get => _bigRoomDivisionName;
            set
            {
                _bigRoomDivisionName = value;
                OnPropertyChanged(nameof(BigRoomDivisionName));
            }
        }
        public string? RoomDivision1
        {
            get => _roomDivision1;
            set
            {
                _roomDivision1 = value;
                OnPropertyChanged(nameof(RoomDivision1));
            }
        }
        public string? RoomDivision2
        {
            get => _roomDivision2;
            set
            {
                _roomDivision2 = value;
                OnPropertyChanged(nameof(RoomDivision2));
            }
        }
        public List<Room> Rooms1
        {
            get => _rooms1;
            set
            {
                _rooms1 = value;
                OnPropertyChanged(nameof(Rooms1));
            }
        }
        public List<Room> Rooms2
        {
            get => _rooms2;
            set
            {
                _rooms2 = value;
                OnPropertyChanged(nameof(Rooms2));
            }
        }
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                GlobalStore.AddObject("SelectedRoom", value);
                if (value != null)
                {
                    BigRoomDivisionName = value.Name;
                }
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        public Room SelectedRoomMerge1
        {
            get => _selectedRoomMerge1;
            set
            {
                _selectedRoomMerge1 = value;
                GlobalStore.AddObject("SelectedRoomMerge1", value);
                OnPropertyChanged(nameof(SelectedRoomMerge1));
            }
        }
        public Room SelectedRoomMerge2
        {
            get => _selectedRoomMerge2;
            set
            {
                _selectedRoomMerge2 = value;
                GlobalStore.AddObject("SelectedRoomMerge2", value);
                OnPropertyChanged(nameof(SelectedRoomMerge2));
            }
        }

        #endregion

        #region Commands

        public ICommand? StandardRenovationCommand { get; }
        public ICommand? DivideRenovationCommand { get; }
        public ICommand? MergeRenovationCommand { get; }

        #endregion

        #region Methods

        void LoadRooms()
        {
            _rooms = _roomRepository.ReadAll().ToList();
            _rooms = _rooms.OrderBy(x => x.Name).ToList();
            _selectedRoom = null;

            _rooms1 = _roomRepository.ReadAll().ToList();
            _rooms1 = _rooms1.OrderBy(x => x.Name).ToList();
            _selectedRoomMerge1 = null;

            _rooms2 = _roomRepository.ReadAll().ToList();
            _rooms2 = _rooms2.OrderBy(x => x.Name).ToList();
            _selectedRoomMerge2 = null;
        }

        #endregion

        public RoomRenovationViewModel(IRoomRepository roomRepository, ISchedulingService schedulingService,
            IRoomRenovationRepository roomRenovationRepository, IRoomService roomService)
        {
            _startDate = DateTime.Now;
            _endDate = DateTime.Now;

            _roomRepository = roomRepository;
            _schedulingService = schedulingService;
            _roomService = roomService;
            _roomRenovationRepository = roomRenovationRepository;
            Rooms = roomRepository.ReadAll().ToList();
            Rooms = Rooms.OrderBy(x => x.Name).ToList();
            Rooms1 = roomRepository.ReadAll().ToList();
            Rooms1 = Rooms1.OrderBy(x => x.Name).ToList();
            Rooms2 = roomRepository.ReadAll().ToList();
            Rooms2 = Rooms2.OrderBy(x => x.Name).ToList();

            SelectedRoom = null;
            SelectedRoomMerge1 = null;
            SelectedRoomMerge2 = null;

            StandardRenovationCommand = new StandardRenovationCommand(this, schedulingService, roomRenovationRepository);
            DivideRenovationCommand = new DivideRenovationCommand(this, schedulingService, roomRenovationRepository, roomService);
            MergeRenovationCommand = new MergeRenovationCommand(this, schedulingService, roomRenovationRepository, roomService);

        }
    }

}
