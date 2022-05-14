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
    public class RoomRenovationViewModel : ViewModelBase
    {
        public readonly IRoomService roomService;
        public readonly IAppointmentService appointmentService;
        public readonly IRoomRenovationService roomRenovationService;
        public ICommand? StandardRenovationCommand { get; }

        public ICommand? DivideRenovationCommand { get; }

        public ICommand? MergeRenovationCommand { get; }

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

        private string _bigRoomDivisionName;
        public string? BigRoomDivisionName
        {
            get => _bigRoomDivisionName;
            set
            {
                _bigRoomDivisionName = value;
                OnPropertyChanged(nameof(BigRoomDivisionName));
            }
        }

        private string _roomDivision1;
        public string? RoomDivision1
        {
            get => _roomDivision1;
            set
            {
                _roomDivision1 = value;
                OnPropertyChanged(nameof(RoomDivision1));
            }
        }

        private string _roomDivision2;
        public string? RoomDivision2
        {
            get => _roomDivision2;
            set
            {
                _roomDivision2 = value;
                OnPropertyChanged(nameof(RoomDivision2));
            }
        }

        private List<Room> _rooms1;

        public List<Room> Rooms1
        {
            get => _rooms1;
            set
            {
                _rooms1 = value;
                OnPropertyChanged(nameof(Rooms1));
            }
        }

        private List<Room> _rooms2;

        public List<Room> Rooms2
        {
            get => _rooms2;
            set
            {
                _rooms2 = value;
                OnPropertyChanged(nameof(Rooms2));
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
                if (value != null)
                {
                    BigRoomDivisionName = value.Name;
                }
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private Room _selectedRoomMerge1;
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

        private Room _selectedRoomMerge2;
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

        public RoomRenovationViewModel(IRoomService roomService, IAppointmentService appointmentService, 
            IRoomRenovationService roomRenovationService)
        {
            roomService = roomService;
            appointmentService = appointmentService;
            roomRenovationService = roomRenovationService;
            Rooms = roomService.ReadAll().ToList();
            Rooms1 = roomService.ReadAll().ToList();
            Rooms2 = roomService.ReadAll().ToList();
            
            SelectedRoom = null;
            SelectedRoomMerge1 = null;
            SelectedRoomMerge2 = null;

            StandardRenovationCommand = new StandardRenovationCommand(this, appointmentService, roomRenovationService);
            DivideRenovationCommand = new DivideRenovationCommand(this, appointmentService, roomRenovationService, roomService);
            MergeRenovationCommand = new MergeRenovationCommand(this, appointmentService, roomRenovationService, roomService);

            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }
    }
 
}
