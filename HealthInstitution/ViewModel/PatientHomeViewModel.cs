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
        public ICommand LogOutCommand { get; set; }
        public ICommand? PatientAppointmentsCommand { get; }

        public string PatientName 
        { 
            get => GlobalStore.ReadObject<Patient>("LoggedUser").FirstName; 
        }

        public PatientHomeViewModel(IPatientService patientService)
        {
            PatientAppointmentsCommand = new PatientAppointmentsCommand();
            LogOutCommand = new LogOutCommand();
            SwitchCurrentViewModel(ServiceLocator.Get<PatientAppointmentsViewModel>());
            RegisterHandler();
        }

        private void RegisterHandler()
        {
            EventBus.RegisterHandler("PatientAppointments", () =>
            {
                PatientAppointmentsViewModel Pavm = ServiceLocator.Get<PatientAppointmentsViewModel>();
                SwitchCurrentViewModel(Pavm);
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
    }
}
