﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.Core.Features.RoomManagement.Repository;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.RoomManagement;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD
{
    public class MergeRenovationCommand : CommandBase
    {
        private Room _selectedRoom;
        private ISchedulingService _schedulingService;
        private IRoomRenovationRepository _roomRenovationRepository;
        private IRoomService _roomService;
        private readonly RoomRenovationViewModel? _viewModel;
        public MergeRenovationCommand(RoomRenovationViewModel viewModel, ISchedulingService schedulingService,
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
            if (e.PropertyName == nameof(_viewModel.SelectedRoomMerge1) || e.PropertyName == nameof(_viewModel.SelectedRoomMerge2) ||
                e.PropertyName == nameof(_viewModel.StartDate) || e.PropertyName == nameof(_viewModel.EndDate))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_viewModel.SelectedRoomMerge1 == null) && !(_viewModel.SelectedRoomMerge2 == null) &&
                !_viewModel.SelectedRoomMerge1.Equals(_viewModel.SelectedRoomMerge2) &&
                !(_viewModel.StartDate < DateTime.Now || _viewModel.StartDate == DateTime.Now) &&
                !(_viewModel.EndDate < _viewModel.StartDate || _viewModel.EndDate == _viewModel.StartDate) &&
                (_viewModel.SelectedRoomMerge1.RoomType == _viewModel.SelectedRoomMerge2.RoomType) &&
                base.CanExecute(parameter);
        }

        private bool HasAppointments(Room room)
        {
            var apts = _schedulingService.ReadRoomAppointments(room);
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
        private bool HasScheduledRenovation(Room room1, Room room2)
        {
            var renRooms = _roomRenovationRepository.ReadAll();
            foreach (var renRoom in renRooms)
            {
                if (renRoom.RenovatedRoom.Name.Equals(room1.Name) || renRoom.RenovatedRoom.Name.Equals(room2.Name))
                {
                    MessageBox.Show("Chosen room is already scheduled for renovation!");
                    return true;
                }
                if (renRoom.AdvancedDivide != null)
                {
                    if (renRoom.RenovatedSmallRoom1Name.Equals(room1.Name) || renRoom.RenovatedSmallRoom1Name.Equals(room2.Name) ||
                        renRoom.RenovatedSmallRoom2Name.Equals(room1.Name) || renRoom.RenovatedSmallRoom2Name.Equals(room2.Name))
                    {
                        MessageBox.Show("Chosen room is already scheduled for renovation!");
                        return true;
                    }
                }
            }
            return false;
        }

        public override void Execute(object? parameter)
        {
            Room smallRoom1 = GlobalStore.ReadObject<Room>("SelectedRoomMerge1"); ;
            Room smallRoom2 = GlobalStore.ReadObject<Room>("SelectedRoomMerge2"); ;

            if (HasAppointments(smallRoom1)) { return; }
            if (HasAppointments(smallRoom2)) { return; }
            if (HasScheduledRenovation(smallRoom1, smallRoom2)) { return; }

            RoomRenovation roomRenovation = new RoomRenovation(smallRoom1, _viewModel.StartDate, _viewModel.EndDate, false, smallRoom1.Name, smallRoom2.Name);
            _roomRenovationRepository.Create(roomRenovation);
            MessageBox.Show("Room renovation has been successfully scheduled!");

        }
    }
}