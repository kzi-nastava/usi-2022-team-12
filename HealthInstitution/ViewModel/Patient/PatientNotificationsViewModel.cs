using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class PatientNotificationsViewModel : ViewModelBase
    {
        #region services
        private readonly IPatientService _patientService;
        public IPatientService PatientService => _patientService;
        #endregion

        #region attributes
        private int _notificationPreference;
        public int NotificationPreference
        {
            get => _notificationPreference;
            set
            {
                _notificationPreference = value;
                OnPropertyChanged(nameof(NotificationPreference));
            }
        }
        #endregion

        #region commands
        public ICommand? SaveNotificationPreferenceCommand { get; }
        #endregion

        public PatientNotificationsViewModel(IPatientService patientService) { 
            _patientService = patientService;
            NotificationPreference = GlobalStore.ReadObject<Patient>("LoggedUser").NotificationPreference;
            SaveNotificationPreferenceCommand = new SaveNotificationPreferenceCommand(this);
        }
    }
}
