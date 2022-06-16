using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using HealthInstitution.Core.Features.NotificationManagement.Model;
using HealthInstitution.Core.Features.NotificationManagement.Service;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Utility.Checker
{
    public static class NotificationsChecker
    {
        private static Timer _timer;
        private static int _checksCounter;
        private static readonly IPrescribedMedicineNotificationService _prescribedMedicineNotificationService;
        private static readonly IUserNotificationService _userNotificationService;
        static NotificationsChecker()
        {
            _prescribedMedicineNotificationService = new PrescribedMedicineNotificationService(new DatabaseContext(0));
            _userNotificationService = new UserNotificationService(new DatabaseContext(0), 0);
        }

        public static void InitializeTimer(Type type)
        {
            _checksCounter = 0;
            StopTimer();

            if (type == typeof(Patient))
            {
                _timer = new Timer(CheckForUpcomingPatientNotification, null, 1000, 60000);
            }
            else
            {
                _timer = new Timer(CheckNotificationsForUser, null, 1000, 60000);
            }
        }

        public static void StopTimer()
        {
            if (_timer != null)
            {
                _timer.Dispose();
            }
        }

        public static void CheckForUpcomingPatientNotification(object stateInfo)
        {
            Patient patient = GlobalStore.ReadObject<Patient>("LoggedUser");
            var nextMedicineNotification = _prescribedMedicineNotificationService.GetNextMedicineNotification(patient);
            if (nextMedicineNotification != null && nextMedicineNotification.TriggerTime < DateTime.Now)
            {
                nextMedicineNotification.Triggered = true;
                _prescribedMedicineNotificationService.Update(nextMedicineNotification);
                MessageBox.Show("You should drink " + nextMedicineNotification.PrescribedMedicine.Medicine.Name + " at " + nextMedicineNotification.TriggerTime.AddMinutes(patient.NotificationPreference) +
                    "\nInstructions: " + nextMedicineNotification.PrescribedMedicine.Instruction, "Reminder");
            }

            if (_checksCounter % 10 == 0)
            {
                _prescribedMedicineNotificationService.CreateUpcomingMedicinesNotifications(patient, patient.NotificationPreference);
            }
            _checksCounter++;

            CheckNotificationsForUser(stateInfo);
        }

        public static void CheckNotificationsForUser(object stateInfo)
        {
            Guid userId = GlobalStore.ReadObject<User>("LoggedUser").Id;

            IList<UserNotification> notifications = _userNotificationService.GetValidNotificationsForUser(userId);
            if (notifications.Count == 0) return;

            foreach (var notification in notifications)
            {
                MessageBox.Show(notification.Content);

                notification.IsShown = true;
                _userNotificationService.Update(notification);
            }
        }
    }
}
