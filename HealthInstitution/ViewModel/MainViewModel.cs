using HealthInstitution.Ninject;
using HealthInstitution.Utility;
using System.Windows;

namespace HealthInstitution.ViewModel
{
    public class MainViewModel : NavigableViewModel
    { 
        private string _viewTitle;
        public string ViewTitle
        {
            get => _viewTitle;
        }
        private void OnTitleChanged()
        {
            _viewTitle = TitleManager.Title;
            OnPropertyChanged(nameof(ViewTitle));
        }
        public LoginViewModel Lvm { get; set; }
        public MainViewModel(LoginViewModel lvm)
        {
            TitleManager.TitleChanged += OnTitleChanged;
            TitleManager.Title = "Login";
            Lvm = lvm;
            SwitchCurrentViewModel(lvm);
            RegisterHandler();
        }
        private void RegisterHandler()
        {
            EventBus.RegisterHandler("PatientLogin", () =>
            {
                PatientHomeViewModel Phvm = ServiceLocator.Get<PatientHomeViewModel>();
                SwitchCurrentViewModel(Phvm);
            });

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
                Lvm = ServiceLocator.Get<LoginViewModel>();
                SwitchCurrentViewModel(Lvm);
                RegisterHandler();
            });
        }
    }
}
