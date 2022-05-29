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
        private readonly PatientNotificationsViewModel? _viewModel;
        public SaveNotificationPreferenceCommand(PatientNotificationsViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            Patient patient = GlobalStore.ReadObject<Patient>("LoggedUser");
            patient.NotificationPreference = _viewModel.NotificationPreference;
            _viewModel.PatientService.Update(patient);
            MessageBox.Show("Your preference has been saved!");
        }
    }
}
