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
    public class NotificationsChecker
    {
        private readonly Timer _timer;
        private readonly INotificationService _notificationService;
        public NotificationsChecker() {
            _notificationService = ServiceLocator.Get<INotificationService>();
            var autoEvent = new AutoResetEvent(false);
            _timer = new Timer(CheckForPatientNotifications, autoEvent, 1000, 4000);
        }

        public void CheckForPatientNotifications(Object stateInfo) {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;

            Patient patient = GlobalStore.ReadObject<Patient>("LoggedUser");
            List<PrescribedMedicine> upcomingMedicines = _notificationService.FilterNextMedications(patient, patient.NotificationPreference);
            if (upcomingMedicines.Count > 0) {
                foreach (var upcomingMedicine in upcomingMedicines)
                {
                    MessageBox.Show("It is time to drink " + upcomingMedicine.Medicine.Name + "\nInstructions: " + upcomingMedicine.Instruction, "Reminder");
                }
            }
        }
    }
}
