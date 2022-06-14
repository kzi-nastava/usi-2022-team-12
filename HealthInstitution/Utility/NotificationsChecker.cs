using HealthInstitution.Model;
using HealthInstitution.Ninject;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using HealthInstitution.Model.user;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.Utility
{
    public static class NotificationsChecker
    {
        private static Timer _timer;
        private static int _checksCounter;
        private static readonly IPrescribedMedicineNotificationService _prescribedMedicineNotificationService;
        static NotificationsChecker()
        {
            _prescribedMedicineNotificationService = new PrescribedMedicineNotificationService(new DatabaseContext(0));
        }

        public static void InitializeTimer(Type type)
        {
            _checksCounter = 0;
            StopTimer();

            if (type == typeof(Patient))
            {
                _timer = new Timer(CheckForUpcomingNotification, null, 1000, 60000);
            }
        }

        public static void StopTimer()
        {
            if (_timer != null)
            {
                _timer.Dispose();
            }
        }

        public static void CheckForUpcomingNotification(Object stateInfo)
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

            if (_checksCounter % 10 == 0) {
                _prescribedMedicineNotificationService.CreateUpcomingMedicinesNotifications(patient, patient.NotificationPreference);
            }
            _checksCounter++;
        }
    }
}
