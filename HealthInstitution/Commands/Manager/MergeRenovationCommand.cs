﻿using HealthInstitution.Model;
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
    public class MergeRenovationCommand : CommandBase
    {
        private Room _selectedRoom;
        private IAppointmentService _appointmentService;
        private IRoomRenovationService _roomRenovationService;
        private IRoomService _roomService;
        private readonly RoomRenovationViewModel? _viewModel;
        public MergeRenovationCommand(RoomRenovationViewModel viewModel, IAppointmentService appointmentService,
            IRoomRenovationService roomRenovationService, IRoomService roomService)
        {
            _appointmentService = appointmentService;
            _roomRenovationService = roomRenovationService;
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
                base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            Room smallRoom1 = GlobalStore.ReadObject<Room>("SelectedRoomMerge1"); ;
            Room smallRoom2 = GlobalStore.ReadObject<Room>("SelectedRoomMerge2"); ;

            var apts1 = _appointmentService.ReadRoomAppointments(smallRoom1);
            var apts2 = _appointmentService.ReadRoomAppointments(smallRoom2);
            var renRooms = _roomRenovationService.ReadAll();

            if (apts1.Count() != 0 || apts2.Count() != 0)
            {
                MessageBox.Show("Chosen room has appointments!");
                return;
            }

            foreach (var renRoom in renRooms)
            {
                if (renRoom.RenovatedRoom.Name.Equals(smallRoom1.Name) || renRoom.RenovatedRoom.Name.Equals(smallRoom2.Name))
                {
                    MessageBox.Show("Chosen room is already scheduled for renovation!");
                    return;
                }
                if (renRoom.AdvancedDivide != null)
                {
                    if (renRoom.RenovatedSmallRoom1Name.Equals(smallRoom1.Name) || renRoom.RenovatedSmallRoom1Name.Equals(smallRoom2.Name) || 
                        renRoom.RenovatedSmallRoom2Name.Equals(smallRoom1.Name) || renRoom.RenovatedSmallRoom2Name.Equals(smallRoom2.Name))
                    {
                        MessageBox.Show("Chosen room is already scheduled for renovation!");
                        return;
                    }
                }
            }

            RoomRenovation roomRenovation = new RoomRenovation(smallRoom1, _viewModel.StartDate, _viewModel.EndDate, false, smallRoom1.Name, smallRoom2.Name);
            _roomRenovationService.Create(roomRenovation);
            MessageBox.Show("Room renovation has been successfully scheduled!");

        }
    }
}