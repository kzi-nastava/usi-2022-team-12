using HealthInstitution.Commands;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.Model;
using HealthInstitution.Ninject;
using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using HealthInstitution.Commands.doctor.Navigation;
using HealthInstitution.Model.appointment;
using HealthInstitution.Model.user;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.ViewModel.doctor;

namespace HealthInstitution.ViewModel
{
    public class DoctorHomeViewModel : NavigableViewModel
    {
        public ICommand? LogOutCommand { get; }
        public ICommand? NavigateScheduleCommand { get; }
        public ICommand? NavigateMedicineCommand { get; }
        public ICommand? NavigateOffDaysCommand { get; }

        private readonly INotificationService _notificationService;

        public string LastName
        {
            get => GlobalStore.ReadObject<Doctor>("LoggedUser").LastName;
        }
        public DoctorHomeViewModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
            LogOutCommand = new LogOutCommand();
            NavigateMedicineCommand = new NavigateMedicineCommand();
            NavigateScheduleCommand = new NavigateScheduleCommand();
            NavigateOffDaysCommand = new NavigateOffDaysCommand();
            SwitchCurrentViewModel(ServiceLocator.Get<DoctorScheduleViewModel>());
            RegisterHandler();
            CheckNotifications();
        }

        private void RegisterHandler()
        {
            DoctorMedicineManagmentViewModel doctorMedicineManagmentViewModel = ServiceLocator.Get<DoctorMedicineManagmentViewModel>();
            EventBus.RegisterHandler("DoctorMedicineManagment", () =>
            {
                SwitchCurrentViewModel(doctorMedicineManagmentViewModel);
            });
            DoctorScheduleViewModel doctorScheduleViewModel = ServiceLocator.Get<DoctorScheduleViewModel>();
            EventBus.RegisterHandler("DoctorSchedule", () =>
            {
                SwitchCurrentViewModel(doctorScheduleViewModel);
            });
            EventBus.RegisterHandler("DoctorOffDays", () =>
            {
                DoctorOffDaysViewModel doctorOffDaysViewModel = ServiceLocator.Get<DoctorOffDaysViewModel>();
                SwitchCurrentViewModel(doctorOffDaysViewModel);
            });
            EventBus.RegisterHandler("CreateOffDayRequest", () =>
            {
                CreateOffDayRequestViewModel createOffDayRequestViewModel = ServiceLocator.Get<CreateOffDayRequestViewModel>();
                SwitchCurrentViewModel(createOffDayRequestViewModel);
            });
            EventBus.RegisterHandler("MedicalRecord", () =>
            {
                MedicalRecordViewModel viewModel = new(ServiceLocator.Get<IMedicalRecordService>(),
                                                        ServiceLocator.Get<IAppointmentService>(),
                                                        GlobalStore.ReadObject<Patient>("SelectedPatient"));
                SwitchCurrentViewModel(viewModel);
            });
            EventBus.RegisterHandler("CreateRefferal", () =>
            {
                DoctorReferralCreationViewModel viewModel = new(ServiceLocator.Get<IDoctorService>(), GlobalStore.ReadObject<Patient>("SelectedPatient"));
                SwitchCurrentViewModel(viewModel);
            });
            EventBus.RegisterHandler("CreatePrescription", () =>
            {
                IMedicalRecordService medicalRecordService = ServiceLocator.Get<IMedicalRecordService>();
                MedicalRecord medicalRecord = medicalRecordService.GetMedicalRecordForPatient(GlobalStore.ReadObject<Patient>("SelectedPatient"));
                PrescriptionViewModel viewModel = new(ServiceLocator.Get<IMedicineService>(), medicalRecord);
                SwitchCurrentViewModel(viewModel);
            });
            ExaminationViewModel? viewModel = null;
            EventBus.RegisterHandler("Examination", () =>
            {
                viewModel = new(ServiceLocator.Get<IMedicalRecordService>(),
                                ServiceLocator.Get<IIllnessService>(),
                                ServiceLocator.Get<IAllergenService>(),
                                ServiceLocator.Get<IAppointmentService>(),
                                ServiceLocator.Get<IReferralService>(),
                                ServiceLocator.Get<IPrescribedMedicineService>(),
                                ServiceLocator.Get<IDialogService>(),
                                ServiceLocator.Get<IEntryService>(),
                                GlobalStore.ReadObject<Appointment>("SelectedAppointment"));
                SwitchCurrentViewModel(viewModel);
            });
            EventBus.RegisterHandler("ReturnToExamination", () =>
            {
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

        public void CheckNotifications()
        {
            Guid userId = GlobalStore.ReadObject<Doctor>("LoggedUser").Id;

            IList<Notification> notifications = _notificationService.GetValidNotificationsForUser(userId);

            if (notifications.Count != 0)
            {
                foreach (var notification in notifications)
                {
                    MessageBox.Show(notification.Content);

                    notification.IsShown = true;
                    _notificationService.Update(notification);
                }
            }
        }
    }
}
