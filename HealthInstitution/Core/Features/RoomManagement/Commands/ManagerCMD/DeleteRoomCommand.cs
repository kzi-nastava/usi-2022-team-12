﻿using System.ComponentModel;
using System.Linq;
using System.Windows;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Repository;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.RoomManagement;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD
{
    public class DeleteRoomCommand : CommandBase
    {
        private readonly RoomsCRUDViewModel? _viewModel;
        private Room _selectedRoom;
        private ISchedulingService _schedulingService;
        private IRoomRepository _roomRepository;

        public DeleteRoomCommand(RoomsCRUDViewModel viewModel, ISchedulingService schedulingService, IRoomRepository roomRepository)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
            _schedulingService = schedulingService;
            _roomRepository = roomRepository;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SelectedRoom))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedRoom != null;
        }

        public override void Execute(object? parameter)
        {
            _selectedRoom = GlobalStore.ReadObject<Room>("SelectedRoom");
            var apts = _schedulingService.ReadRoomAppointments(_selectedRoom);
            

            if (_selectedRoom.Inventory.Count() != 0)
            {
                MessageBox.Show("Room with that name has equipment!");
            }
            else if (apts.Count() != 0)
            {
                MessageBox.Show("Room with that name has appointments!");
            }
            else 
            {
                _roomRepository.Delete(_selectedRoom.Id);
                MessageBox.Show("Room deleted successfully!");
                EventBus.FireEvent("RoomsOverview");
            }

        }
    }
}