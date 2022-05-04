using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Ninject;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class PatientHomeViewModel : NavigableViewModel
    {
        #region commands
        public ICommand LogOutCommand { get; set; }
        public ICommand? PatientAppointmentsCommand { get; }
        public ICommand? PatientMedicalRecordCommand { get; }
        #endregion

        #region attributes
        public string PatientName 
        { 
            get => GlobalStore.ReadObject<Patient>("LoggedUser").FirstName;
        }
        #endregion

        public PatientHomeViewModel()
        {
            PatientAppointmentsCommand = new PatientAppointmentsCommand();
            PatientMedicalRecordCommand = new PatientMedicalRecordCommand();
            LogOutCommand = new LogOutCommand();
            SwitchCurrentViewModel(ServiceLocator.Get<PatientAppointmentsViewModel>());
            RegisterHandler();
        }


        #region handlers
        private void RegisterHandler()
        {
            EventBus.RegisterHandler("PatientAppointments", () =>
            {
                PatientAppointmentsViewModel Pavm = ServiceLocator.Get<PatientAppointmentsViewModel>();
                SwitchCurrentViewModel(Pavm);
            });

            EventBus.RegisterHandler("PatientMedicalRecord", () =>
            {
                PatientMedicalRecordViewModel Pmrvm = ServiceLocator.Get<PatientMedicalRecordViewModel>();
                SwitchCurrentViewModel(Pmrvm);
            });

            EventBus.RegisterHandler("AppointmentCreation", () =>
            {
                AppointmentCreationViewModel Acvm = ServiceLocator.Get<AppointmentCreationViewModel>();
                SwitchCurrentViewModel(Acvm);
            });

            EventBus.RegisterHandler("AppointmentUpdate", () =>
            {
                AppointmentUpdateViewModel Auvm = ServiceLocator.Get<AppointmentUpdateViewModel>();
                SwitchCurrentViewModel(Auvm);
            });
        }
        #endregion
    }
}
