﻿using System.ComponentModel;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.RoomManagement;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD
{
    public class ConfirmRoomChoiceCommand : CommandBase
    {
        private readonly RoomChoiceViewModel? _viewModel;
        public ConfirmRoomChoiceCommand(RoomChoiceViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SelectedRoom1) || e.PropertyName == nameof(_viewModel.SelectedRoom2))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !_viewModel.SelectedRoom1.Equals(_viewModel.SelectedRoom2) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            GlobalStore.AddObject("ArrangeRoom1", _viewModel.SelectedRoom1);
            GlobalStore.AddObject("ArrangeRoom2", _viewModel.SelectedRoom2);
            GlobalStore.AddObject("ArrangeTime", _viewModel.ArrangeDateTime);
            EventBus.FireEvent("RoomsConfirmed");
        }
    }
}