using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthInstitution.Commands
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

            foreach (var renRoom in renRooms)
            {
                if (renRoom.RenovatedRoom.Name.Equals(_selectedRoom.Name))
                {
                    MessageBox.Show("Chosen room is already scheduled for renovation!");
                    return;
                }
            }
            if (apts.Count() != 0)
            {
                MessageBox.Show("Chosen room has appointments!");
            }
            else
            {
                RoomRenovation roomRenovation = new RoomRenovation(_selectedRoom, _viewModel.StartDate, _viewModel.EndDate);
                _roomRenovationService.Create(roomRenovation);
                MessageBox.Show("Room renovation has been successfully scheduled!");

            }
        }
    }
}