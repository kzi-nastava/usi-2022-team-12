using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Ninject;
using HealthInstitution.Services.Implementation;
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
    internal class DoctorHomeViewModel : NavigableViewModel
    {
        public ICommand? LogOutCommand { get; }
        public ICommand? NavigateScheduleCommand { get; }

        public string FullName
        {
            get => GlobalStore.ReadObject<Doctor>("LoggedUser").FullName;
        }
        public DoctorHomeViewModel()
        {
            LogOutCommand = new LogOutCommand();
            NavigateScheduleCommand = new NavigateScheduleCommand();
            SwitchCurrentViewModel(ServiceLocator.Get<DoctorScheduleViewModel>());
            RegisterHandler();
        }

        private void RegisterHandler()
        {
            DoctorScheduleViewModel doctorScheduleViewModel = ServiceLocator.Get<DoctorScheduleViewModel>();
            EventBus.RegisterHandler("DoctorSchedule", () =>
            {
                SwitchCurrentViewModel(doctorScheduleViewModel);
            });
            EventBus.RegisterHandler("MedicalRecord", () =>
            {
                MedicalRecordViewModel viewModel = new(ServiceLocator.Get<IMedicalRecordService>(),
                                                        ServiceLocator.Get<IAppointmentService>(),
                                                        GlobalStore.ReadObject<Patient>("SelectedPatient"));
                SwitchCurrentViewModel(viewModel);
            });
            EventBus.RegisterHandler("Examination", () =>
            {
                ExaminationViewModel viewModel = new(ServiceLocator.Get<IMedicalRecordService>(),
                                                    ServiceLocator.Get<IIllnessService>(),
                                                    ServiceLocator.Get<IAllergenService>(),
                                                    ServiceLocator.Get<IAppointmentService>(),
                                                    GlobalStore.ReadObject<Appointment>("SelectedAppointment"));
                SwitchCurrentViewModel(viewModel);
            });
            EventBus.RegisterHandler("CreateAppointment", () =>
            {
                DoctorAppointmentCreationViewModel viewModel = new(ServiceLocator.Get<IPatientService>(), ServiceLocator.Get<IAppointmentService>());
                SwitchCurrentViewModel(viewModel);
            });
            EventBus.RegisterHandler("UpdateAppointment", () =>
            {
                DoctorAppointmentUpdateViewModel viewModel = new(ServiceLocator.Get<IPatientService>(), ServiceLocator.Get<IAppointmentService>(), GlobalStore.ReadObject<Appointment>("SelectedAppointment"));
                SwitchCurrentViewModel(viewModel);
            });
        }
    }
}
