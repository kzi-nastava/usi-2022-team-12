using HealthInstitution.Model;
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
    public class AddRoomCommand : CommandBase
    {

        private readonly RoomCreateViewModel? _viewModel;
        public AddRoomCommand(RoomCreateViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.RoomName) || e.PropertyName == nameof(_viewModel.SelectedType))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.RoomName) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            string roomName = _viewModel.RoomName;
            var rooms = _viewModel._roomService.ReadRoomsWithName(roomName);

            if (rooms.Count() == 0)
            {
                Room r = new Room(_viewModel.SelectedType, roomName);
                _viewModel._roomService.Create(r);
                MessageBox.Show("Room created successfully!");
                EventBus.FireEvent("RoomsOverview");
            }
            else
            {
                MessageBox.Show("Room with that name already exists!");
                return;
            }

            EventBus.FireEvent("AddRoom");
        }
    }
}