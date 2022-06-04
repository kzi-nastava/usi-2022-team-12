using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Ninject;
using HealthInstitution.Utility;
using System.Windows.Input;
using HealthInstitution.Model.user;
using HealthInstitution.ViewModel.secretary;

namespace HealthInstitution.ViewModel
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

            RegisterHandler();
        }


        private void RegisterHandler()
        {
        }



    }
}
