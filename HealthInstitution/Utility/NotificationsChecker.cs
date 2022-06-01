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
        private static bool _databaseBusy;
        private static readonly IPrescribedMedicineNotificationService _prescribedMedicineNotificationService;
        static NotificationsChecker()
        {
            _prescribedMedicineNotificationService = ServiceLocator.Get<IPrescribedMedicineNotificationService>();
        }

        public static void InitializeTimer(string loggedEntity)
        {
            _databaseBusy = false;
            StopTimer();

            if (loggedEntity == "patient")
            {
                _notificationsUpdateTimer = new Timer(UpdatePrescribedMedicationsNotifications, null, 1000, Timeout.Infinite);
                _notificationsTriggerTimer = new Timer(CheckForUpcomingNotification, null, 1000, Timeout.Infinite);
            }
        }

        public static void StopTimer()
        {
            if (_notificationsTriggerTimer != null)
            {
                _notificationsTriggerTimer.Dispose();
            }
            if (_notificationsUpdateTimer != null)
            {
                _notificationsUpdateTimer.Dispose();
            }
        }

        public static void UpdatePrescribedMedicationsNotifications(Object stateInfo)
        {
            if (!_databaseBusy)
            {
                _databaseBusy = true;
                Patient patient = GlobalStore.ReadObject<Patient>("LoggedUser");
                _prescribedMedicineNotificationService.CreateUpcomingMedicationsNotifications(patient, patient.NotificationPreference);
                _databaseBusy = false;
                _notificationsUpdateTimer.Change(300000, Timeout.Infinite);
            }
            else {
                _notificationsUpdateTimer.Change(1000, Timeout.Infinite);
            }
        }

        public static void CheckForUpcomingNotification(Object stateInfo)
        {
            if (!_databaseBusy)
            {
                _databaseBusy = true;
                Patient patient = GlobalStore.ReadObject<Patient>("LoggedUser");
                var nextMedicineNotification = _prescribedMedicineNotificationService.GetNextMedicineNotification(patient);
                if (nextMedicineNotification != null && nextMedicineNotification.TriggerTime < DateTime.Now)
                {
                    MessageBox.Show("You should drink " + nextMedicineNotification.PrescribedMedicine.Medicine.Name + " at " + nextMedicineNotification.TriggerTime.AddMinutes(patient.NotificationPreference) +
                        "\nInstructions: " + nextMedicineNotification.PrescribedMedicine.Instruction, "Reminder");
                    nextMedicineNotification.Triggered = true;
                    _prescribedMedicineNotificationService.Update(nextMedicineNotification);
                }
                _databaseBusy = false;
                _notificationsTriggerTimer.Change(60000, Timeout.Infinite);
            }
            else {
                _notificationsTriggerTimer.Change(1000, Timeout.Infinite);
            }
        }
    }
}
