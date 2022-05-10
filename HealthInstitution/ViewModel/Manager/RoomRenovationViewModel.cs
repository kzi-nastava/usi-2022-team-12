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

        public RoomRenovationViewModel(IRoomService roomService, IAppointmentService appointmentService, IRoomRenovationService roomRenovationService)
        {
            roomService = roomService;
            appointmentService = appointmentService;
            roomRenovationService = roomRenovationService;
            Rooms = roomService.ReadAll().ToList();
            SelectedRoom = null;

            StandardRenovationCommand = new StandardRenovationCommand(this, appointmentService, roomRenovationService);

            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }
    }
 
}
