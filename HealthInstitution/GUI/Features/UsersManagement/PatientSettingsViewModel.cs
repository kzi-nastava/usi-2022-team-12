using System.Windows.Input;
using HealthInstitution.Core.Features.NotificationManagement.Service;
using HealthInstitution.Core.Features.UsersManagement.Commands.PatientCMD;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Service;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.UsersManagement
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
        #endregion

        #region properties
        public string NotificationPreference
        {
            get => _notificationPreference;
            set
            {
                if (value != "")
                {
                    _notificationPreference = value;
                }
                else
                {
                    _notificationPreference = "1";
                }
                OnPropertyChanged(nameof(NotificationPreference));
            }
        }
        #endregion

        #region commands
        public ICommand? SaveNotificationPreferenceCommand { get; }
        #endregion

        public PatientSettingsViewModel(IPatientService patientService, IPrescribedMedicineNotificationService prescribedMedicineNotificationService)
        {
            _patientService = patientService;
            _prescribedMedicineNotificationService = prescribedMedicineNotificationService;
            NotificationPreference = GlobalStore.ReadObject<Patient>("LoggedUser").NotificationPreference.ToString();
            SaveNotificationPreferenceCommand = new SaveNotificationPreferenceCommand(this);
        }
    }
}
