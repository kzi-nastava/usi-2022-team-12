using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using HealthInstitution.Model.room;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel.manager;

namespace HealthInstitution.Commands.manager
{
    public class StandardRenovationCommand : CommandBase
    {
        private Room _selectedRoom;
        private IAppointmentService _appointmentService;
        private IRoomRenovationService _roomRenovationService;
        private readonly RoomRenovationViewModel? _viewModel;
        public StandardRenovationCommand(RoomRenovationViewModel viewModel, IAppointmentService appointmentService, IRoomRenovationService roomRenovationService)
        {
            _appointmentService = appointmentService;
            _roomRenovationService = roomRenovationService;
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.StartDate) || e.PropertyName == nameof(_viewModel.EndDate) || e.PropertyName == nameof(_viewModel.SelectedRoom))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_viewModel.StartDate < DateTime.Now || _viewModel.StartDate == DateTime.Now) && 
                !(_viewModel.EndDate < _viewModel.StartDate || _viewModel.EndDate == _viewModel.StartDate) && 
                !(_viewModel.SelectedRoom == null) &&
                base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _selectedRoom = GlobalStore.ReadObject<Room>("SelectedRoom");
            var apts = _appointmentService.ReadRoomAppointments(_selectedRoom);
            var renRooms = _roomRenovationService.ReadAll();

            if (apts.Count() != 0)
            {
                foreach (var apt in apts)
                {
                    if (apt.StartDate >= _viewModel.StartDate)
                    {
                        MessageBox.Show("Chosen room has appointments!");
                        return;
                    }
                }
                
            }
            foreach (var renRoom in renRooms)
            {
                if (renRoom.RenovatedRoom.Name.Equals(_selectedRoom.Name))
                {
                    MessageBox.Show("Chosen room is already scheduled for renovation!");
                    return;
                }
                if (renRoom.AdvancedDivide != null)
                {
                    if (renRoom.RenovatedSmallRoom1Name.Equals(_selectedRoom.Name) || renRoom.RenovatedSmallRoom2Name.Equals(_selectedRoom.Name))
                    {
                        MessageBox.Show("Chosen room is already scheduled for renovation!");
                        return;
                    }
                }
            }
            
            
            RoomRenovation roomRenovation = new RoomRenovation(_selectedRoom, _viewModel.StartDate, _viewModel.EndDate);
            _roomRenovationService.Create(roomRenovation);
            MessageBox.Show("Room renovation has been successfully scheduled!");

            
        }
    }
}