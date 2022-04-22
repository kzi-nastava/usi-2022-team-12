using HealthInstitution.Ninject;
using HealthInstitution.Utility;
using System.Windows;

namespace HealthInstitution.ViewModel
{
    public class MainViewModel : NavigableViewModel
    { 
        public LoginViewModel Lvm { get; set; }
        public MainViewModel(LoginViewModel lvm)
        {
            Lvm = lvm;
            SwitchCurrentViewModel(lvm);
            RegisterHandler();
        }
        private void RegisterHandler()
        {

            EventBus.RegisterHandler("SecretaryLogin", () =>
            {
                SecretaryHomeViewModel Shvm = ServiceLocator.Get<SecretaryHomeViewModel>();
                SwitchCurrentViewModel(Shvm);
            });

            EventBus.RegisterHandler("DoctorLogin", () =>
            {
                DoctorHomeViewModel Dhvm = ServiceLocator.Get<DoctorHomeViewModel>();
                SwitchCurrentViewModel(Dhvm);
            });

            EventBus.RegisterHandler("BackToLogin", () =>
            {
                EventBus.Clear();
                ServiceLocator.Reset();
                SwitchCurrentViewModel(Lvm);
                RegisterHandler();
            });
        }
    }
}
