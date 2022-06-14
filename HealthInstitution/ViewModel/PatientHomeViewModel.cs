using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Ninject;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using HealthInstitution.Commands.patient.Navigation;
using HealthInstitution.Model.user;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.ViewModel.patient;

namespace HealthInstitution.ViewModel
{
    public class PatientHomeViewModel : NavigableViewModel
    {
        #region commands
        public ICommand LogOutCommand { get; set; }
        public ICommand? PatientAppointmentsCommand { get; }
        public ICommand? PatientMedicalRecordCommand { get; }
        public ICommand? DoctorSearchCommand { get; }
        public ICommand? PatientSettingsCommand { get; }
        public ICommand? HealthInstitutionSurveyCommand { get; }
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
            PatientSettingsCommand = new PatientSettingsCommand();
            HealthInstitutionSurveyCommand = new HealthInstitutionSurveyCommand();
            LogOutCommand = new LogOutCommand();
            SwitchCurrentViewModel(ServiceLocator.Get<PatientAppointmentsViewModel>());
            RegisterHandler();
            CheckNotifications();

            NotificationsChecker.InitializeTimer(typeof(Patient));
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

            EventBus.RegisterHandler("PatientSettings", () =>
            {
                PatientSettingsViewModel Pnvm = ServiceLocator.Get<PatientSettingsViewModel>();
                SwitchCurrentViewModel(Pnvm);
            });

            EventBus.RegisterHandler("HealthInstitutionSurvey", () =>
            {
                HealthInstitutionSurveyViewModel Hisvm = ServiceLocator.Get<HealthInstitutionSurveyViewModel>();
                SwitchCurrentViewModel(Hisvm);
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

            EventBus.RegisterHandler("RecommendAppointmentCreation", () =>
            {
                RecommendAppointmentCreationViewModel Racvm = ServiceLocator.Get<RecommendAppointmentCreationViewModel>();
                SwitchCurrentViewModel(Racvm);
            });

            EventBus.RegisterHandler("OpenDoctorSurvey", () =>
            {
                DoctorSurveyViewModel Dsvm = ServiceLocator.Get<DoctorSurveyViewModel>();
                SwitchCurrentViewModel(Dsvm);
            });

            EventBus.RegisterHandler("AppointmentCreationWithSelectedDoctor", () =>
            {
                AppointmentCreationViewModel Acvm = new AppointmentCreationViewModel(GlobalStore.ReadObject<Doctor>("SelectedDoctor"));
                SwitchCurrentViewModel(Acvm);
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
