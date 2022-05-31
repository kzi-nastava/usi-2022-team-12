using HealthInstitution.Model;
using HealthInstitution.Ninject;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

namespace HealthInstitution.Utility
{
    public static class NotificationsChecker
    {
        private static Timer _notificationsTriggerTimer;
        private static Timer _notificationsUpdateTimer;
        private static readonly IPrescribedMedicineNotificationService _prescribedMedicineNotificationService;
        static NotificationsChecker() {
            _prescribedMedicineNotificationService = ServiceLocator.Get<IPrescribedMedicineNotificationService>();
        }

        public static void InitializeTimer(string loggedEntity) {
            StopTimer();

            AutoResetEvent autoEvent = new AutoResetEvent(true);
            if (loggedEntity == "patient")
            {
                _notificationsUpdateTimer = new Timer(UpdatePrescribedMedicationsNotifications, autoEvent, 1000, 10000);
                _notificationsTriggerTimer = new Timer(CheckForUpcomingNotification, autoEvent, 1000, 5000);
            }
        }

        public static void StopTimer() {
            if (_notificationsTriggerTimer != null)
            {
                _notificationsTriggerTimer.Dispose();
            }
            if (_notificationsUpdateTimer != null)
            {
                _notificationsUpdateTimer.Dispose();
            }
        }

        public static void UpdatePrescribedMedicationsNotifications(Object stateInfo) {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            autoEvent.WaitOne();

            Patient patient = GlobalStore.ReadObject<Patient>("LoggedUser");
            _prescribedMedicineNotificationService.CreateUpcomingMedicationsNotifications(patient, patient.NotificationPreference);

            autoEvent.Set();
        }

        public static void CheckForUpcomingNotification(Object stateInfo) {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            autoEvent.WaitOne();

            Patient patient = GlobalStore.ReadObject<Patient>("LoggedUser");
            var nextMedicineNotification = _prescribedMedicineNotificationService.GetNextMedicineNotification(patient);
            if (nextMedicineNotification != null && nextMedicineNotification.TriggerTime < DateTime.Now && nextMedicineNotification.Triggered == false) {
                MessageBox.Show("You should drink " + nextMedicineNotification.PrescribedMedicine.Medicine.Name + " at " + nextMedicineNotification.TriggerTime.AddMinutes(patient.NotificationPreference) + 
                    "\nInstructions: " + nextMedicineNotification.PrescribedMedicine.Instruction, "Reminder");
                nextMedicineNotification.Triggered = true;
                _prescribedMedicineNotificationService.Update(nextMedicineNotification);
            }

            autoEvent.Set();
        }
    }
}
