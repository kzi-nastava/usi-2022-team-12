using System.Windows.Input;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Ninject;
using HealthInstitution.GUI.Features.AppointmentScheduling;
using HealthInstitution.GUI.Features.EquipmentManagement;
using HealthInstitution.GUI.Features.OffDaysManagement;
using HealthInstitution.GUI.Features.OperationsAndExaminations;
using HealthInstitution.GUI.Features.UsersManagement;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.Navigation
{
    public class SecretaryHomeViewModel : NavigableViewModel
    {

        #region Commands

        public ICommand LogOutCommand { get; private set; }

        public ICommand ShowValidPatients { get; private set; }

        public ICommand ShowBlockedPatients { get; private set; }

        public ICommand ShowAppointmentRequests { get; private set; }

        public ICommand ShowReferrals { get; private set; }

        public ICommand ShowUrgentScheduling { get; private set; }

        public ICommand ShowDynamicEquipment { get; private set; }

        public ICommand ShowDynamicEquipmentArrangement { get; private set; }

        public ICommand ShowOffDays { get; private set; }

        #endregion

        #region attributes
        public string SecretaryName
        {
            get => GlobalStore.ReadObject<Secretary>("LoggedUser").FirstName;
        }
        #endregion

        public SecretaryHomeViewModel()
        {
            SwitchCurrentViewModel(ServiceLocator.Get<SecretaryPatientCRUDViewModel>());
            LogOutCommand = new LogOutCommand();

            ShowValidPatients = new RelayCommand(() =>
            {
                SwitchCurrentViewModel(ServiceLocator.Get<SecretaryPatientCRUDViewModel>());
            });

            ShowBlockedPatients = new RelayCommand(() =>
            {
                SwitchCurrentViewModel(ServiceLocator.Get<SecretaryBlockedPatientsViewModel>());
            });

            ShowAppointmentRequests = new RelayCommand(() =>
            {
                SwitchCurrentViewModel(ServiceLocator.Get<SecretaryAppointmentRequestsViewModel>());
            });

            ShowReferrals = new RelayCommand(() =>
            {
                SwitchCurrentViewModel(ServiceLocator.Get<SecretaryReferralUsageViewModel>());
            });

            ShowUrgentScheduling = new RelayCommand(() =>
            {
                SwitchCurrentViewModel(ServiceLocator.Get<SecretaryUrgentScheduleViewModel>());
            });

            ShowDynamicEquipment = new RelayCommand(() =>
            {
                SwitchCurrentViewModel(ServiceLocator.Get<SecretaryDynamicEquipmentPurchaseRequestViewModel>());
            });

            ShowDynamicEquipmentArrangement = new RelayCommand(() =>
            {
                SwitchCurrentViewModel(ServiceLocator.Get<SecretaryDynamicEquipmentArrangementViewModel>());
            });

            ShowOffDays = new RelayCommand(() =>
            {
                SwitchCurrentViewModel(ServiceLocator.Get<SecretaryDayOffViewModel>());
            });

            RegisterHandler();
        }


        private void RegisterHandler()
        {
        }



    }
}
