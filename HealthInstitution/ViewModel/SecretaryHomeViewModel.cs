using HealthInstitution.Commands;
using HealthInstitution.Ninject;
using HealthInstitution.Utility;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class SecretaryHomeViewModel : NavigableViewModel
    {
        public ICommand LogOutCommand { get; }

        public SecretaryHomeViewModel()
        {
            SwitchCurrentViewModel(ServiceLocator.Get<SecretaryPatientCRUDViewModel>());
            LogOutCommand = new LogOutCommand();

            RegisterHandler();
        }


        private void RegisterHandler()
        {
        }



    }
}
