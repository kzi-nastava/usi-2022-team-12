using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Repository;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.RoomManagement;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD
{
    public class DivideRenovationCommand : CommandBase
    {
        private Room _selectedRoom;
        private ISchedulingService _schedulingService;
        private IRoomRenovationRepository _roomRenovationRepository;
        private IRoomService _roomService;
        private readonly RoomRenovationViewModel? _viewModel;
        public DivideRenovationCommand(RoomRenovationViewModel viewModel, ISchedulingService schedulingService,
            IRoomRenovationRepository roomRenovationRepository, IRoomService roomService)
        {
            _schedulingService = schedulingService;
            _roomRenovationRepository = roomRenovationRepository;
            _roomService = roomService;
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.RoomDivision1) || e.PropertyName == nameof(_viewModel.RoomDivision2) ||
                e.PropertyName == nameof(_viewModel.StartDate) || e.PropertyName == nameof(_viewModel.EndDate) ||
                e.PropertyName == nameof(_viewModel.SelectedRoom))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.RoomDivision1) && !string.IsNullOrEmpty(_viewModel.RoomDivision2) &&
                !(_viewModel.StartDate < DateTime.Now || _viewModel.StartDate == DateTime.Now) &&
                !(_viewModel.EndDate < _viewModel.StartDate || _viewModel.EndDate == _viewModel.StartDate) &&
                !(_viewModel.SelectedRoom == null) && base.CanExecute(parameter);
        }

        private bool HasAppointments ()
        {
            var apts = _schedulingService.ReadRoomAppointments(_selectedRoom);
            if (apts.Count() != 0)
            {
                foreach (var apt in apts)
                {
                    if (apt.StartDate >= _viewModel.StartDate)
                    {
                        MessageBox.Show("Chosen room has appointments!");
                        return true;
                    }
                }
            }
            return false;
        }
        private bool HasScheduledRenovation()
        {
            var renRooms = _roomRenovationRepository.ReadAll();
            foreach (var renRoom in renRooms)
            {
                if (renRoom.RenovatedRoom.Name.Equals(_selectedRoom.Name))
                {
                    MessageBox.Show("Chosen room is already scheduled for renovation!");
                    return true;
                }
                if (renRoom.AdvancedDivide != null)
                {
                    if (renRoom.RenovatedSmallRoom1Name.Equals(_selectedRoom.Name) || renRoom.RenovatedSmallRoom2Name.Equals(_selectedRoom.Name))
                    {
                        MessageBox.Show("Chosen room is already scheduled for renovation!");
                        return true;
                    }
                }
            }
            return false;
        }
        private bool RoomNamesExist()
        {
            var rooms1 = _roomService.ReadRoomsWithName(_viewModel.RoomDivision1);
            var rooms2 = _roomService.ReadRoomsWithName(_viewModel.RoomDivision2);

            if (rooms1.Count() != 0 || rooms2.Count() != 0)
            {
                MessageBox.Show("Room with that name already exists!");
                return true;
            }
            return false;
        }


        public override void Execute(object? parameter)
        {
            _selectedRoom = GlobalStore.ReadObject<Room>("SelectedRoom");

            if (HasAppointments()) { return; }
            if (HasScheduledRenovation()) { return; }
            if (RoomNamesExist()) { return; }
            
            RoomRenovation roomRenovation = new RoomRenovation(_selectedRoom, _viewModel.StartDate, _viewModel.EndDate, true, _viewModel.RoomDivision1, _viewModel.RoomDivision2);
            _roomRenovationRepository.Create(roomRenovation);
            MessageBox.Show("Room renovation has been successfully scheduled!");
            
        }
    }
}