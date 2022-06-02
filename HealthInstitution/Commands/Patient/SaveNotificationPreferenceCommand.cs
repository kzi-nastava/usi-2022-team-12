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
    public class SaveNotificationPreferenceCommand : CommandBase
    {
        private readonly PatientSettingsViewModel? _viewModel;
        public SaveNotificationPreferenceCommand(PatientSettingsViewModel viewModel)
        {
            _viewModel = viewModel; 
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.NotificationPreference))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_viewModel.NotificationPreference == null) && base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            Patient patient = GlobalStore.ReadObject<Patient>("LoggedUser");
            patient.NotificationPreference = Int32.Parse(_viewModel.NotificationPreference);
            _viewModel.PatientService.Update(patient);
            _viewModel.PrescribedMedicineNotificationService.DeleteUpcomingMedicinesNotifications(patient);
            MessageBox.Show("Your preference has been saved!");
        }
    }
}
