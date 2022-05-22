using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Ninject;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class PatientHomeViewModel : NavigableViewModel
    {
        #region commands
        public ICommand LogOutCommand { get; set; }
        public ICommand? PatientAppointmentsCommand { get; }
        public ICommand? PatientMedicalRecordCommand { get; }
        public ICommand? DoctorSearchCommand { get; }
        #endregion

        #region attributes
        public string PatientName
        {
            get => GlobalStore.ReadObject<Patient>("LoggedUser").FirstName;
        }
        #endregion

        #region Services

        INotificationService _notificationService;

        #endregion

        public PatientHomeViewModel(INotificationService notificationService)
        {
            _notificationService = notificationService;

            PatientAppointmentsCommand = new PatientAppointmentsCommand();
            PatientMedicalRecordCommand = new PatientMedicalRecordCommand();
            DoctorSearchCommand = new DoctorSearchCommand();
            LogOutCommand = new LogOutCommand();
            SwitchCurrentViewModel(ServiceLocator.Get<PatientAppointmentsViewModel>());
            RegisterHandler();
            CheckNotifications();
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

            EventBus.RegisterHandler("DoctorSearch", () =>
            {
                DoctorSearchViewModel Dsvm = ServiceLocator.Get<DoctorSearchViewModel>();
                SwitchCurrentViewModel(Dsvm);
            });

            EventBus.RegisterHandler("RecommendAppointmentCreation", () =>
            {
                RecommendAppointmentCreationViewModel Racvm = ServiceLocator.Get<RecommendAppointmentCreationViewModel>();
                SwitchCurrentViewModel(Racvm);
            });

            EventBus.RegisterHandler("AppointmentCreationWithSelectedDoctor", () =>
            {
                AppointmentCreationViewModel Acvm = new AppointmentCreationViewModel(GlobalStore.ReadObject<Doctor>("SelectedDoctor"));
                SwitchCurrentViewModel(Acvm);
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

        public void CheckNotifications()
        {
            Guid userId = GlobalStore.ReadObject<Patient>("LoggedUser").Id;

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
