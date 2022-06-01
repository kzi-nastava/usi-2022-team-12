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
    public class PatientSettingsViewModel : ViewModelBase
    {
        #region services
        private readonly IPatientService _patientService;

        private readonly IPrescribedMedicineNotificationService _prescribedMedicineNotificationService;

        public IPatientService PatientService => _patientService;
        public IPrescribedMedicineNotificationService PrescribedMedicineNotificationService => _prescribedMedicineNotificationService;
        #endregion

        #region attributes
        private string _notificationPreference;
        public string NotificationPreference
        {
            get => _notificationPreference;
            set
            {
                if (value != "")
                {
                    _notificationPreference = value;
                }
                else {
                    _notificationPreference = "1";
                }
                OnPropertyChanged(nameof(NotificationPreference));
            }
        }
        #endregion

        #region commands
        public ICommand? SaveNotificationPreferenceCommand { get; }
        #endregion

        public PatientSettingsViewModel(IPatientService patientService, IPrescribedMedicineNotificationService prescribedMedicineNotificationService) { 
            _patientService = patientService;
            _prescribedMedicineNotificationService = prescribedMedicineNotificationService;
            NotificationPreference = GlobalStore.ReadObject<Patient>("LoggedUser").NotificationPreference.ToString();
            SaveNotificationPreferenceCommand = new SaveNotificationPreferenceCommand(this);
        }
    }
}
