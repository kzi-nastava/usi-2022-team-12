using HealthInstitution.Commands;
using HealthInstitution.Ninject;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System.Windows.Input;

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

            RegisterHandler();
        }


        private void RegisterHandler()
        {
        }



    }
}
