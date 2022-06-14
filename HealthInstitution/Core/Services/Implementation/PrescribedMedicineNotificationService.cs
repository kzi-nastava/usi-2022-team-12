using HealthInstitution.Model;
using HealthInstitution.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.NotificationManagement.Model;

namespace HealthInstitution.Services.Implementation
{
    public class PrescribedMedicineNotificationService : CrudService<PrescribedMedicineNotification>, IPrescribedMedicineNotificationService
    {
        public PrescribedMedicineNotificationService(DatabaseContext context) : base(context)
        {

        }

        public IEnumerable<PrescribedMedicine> GetConsumingMedications(Patient patient, int notifyMinutesBefore)
        {
            return _context.Set<PrescribedMedicine>().Where(pm => pm.MedicalRecord.Patient == patient && pm.UsageStart.AddMinutes(-notifyMinutesBefore) < DateTime.Now && pm.UsageEnd > DateTime.Now);
        }

        public List<PrescribedMedicineNotification> GenerateUpcomingMedicinesNotifications(Patient patient, int notifyMinutesBefore)
        {
            List<PrescribedMedicineNotification> upcomingMedicinesNotifications = new List<PrescribedMedicineNotification>();
            var currentConsumingMedications = GetConsumingMedications(patient, notifyMinutesBefore).ToList();
            foreach (var prescribedMedicine in currentConsumingMedications)
            {
                var nextTaking = prescribedMedicine.UsageStart.AddMinutes(-notifyMinutesBefore);
                while (nextTaking < DateTime.Now)
                {
                    nextTaking = nextTaking.AddHours(prescribedMedicine.UsageHourSpan);
                }
                if (DateTime.Now < nextTaking)
                {
                    upcomingMedicinesNotifications.Add(new PrescribedMedicineNotification { PrescribedMedicine = prescribedMedicine, TriggerTime = nextTaking, Triggered = false });
                }
            }
            return upcomingMedicinesNotifications;
        }
        public void CreateUpcomingMedicinesNotifications(Patient patient, int notifyMinutesBefore)
        {
            var upcomingMedicinesNotifications = GenerateUpcomingMedicinesNotifications(patient, notifyMinutesBefore);
            foreach (var upcomingMedicineNotification in upcomingMedicinesNotifications)
            {
                if (_entities.Where(pmn => pmn.PrescribedMedicine == upcomingMedicineNotification.PrescribedMedicine && pmn.TriggerTime == upcomingMedicineNotification.TriggerTime).ToList().Count == 0)
                {
                    Create(upcomingMedicineNotification);
                }
            }
        }
        public IEnumerable<PrescribedMedicineNotification> GetUpcomingMedicinesNotifications(Patient patient)
        {
            return _entities.Where(pmn => pmn.PrescribedMedicine.MedicalRecord.Patient == patient && pmn.Triggered == false);
        }

        public void DeleteUpcomingMedicinesNotifications(Patient patient)
        {
            var notificationsToRemove = GetUpcomingMedicinesNotifications(patient);
            foreach (var notification in notificationsToRemove)
            {
                Delete(notification.Id);
            }
        }
        public void DeleteAllUpcomingMedicinesNotifications()
        {
            var notificationsToRemove = _entities.Where(pmn => pmn.Triggered == false);
            foreach (var notification in notificationsToRemove)
            {
                Delete(notification.Id);
            }
        }

        public PrescribedMedicineNotification GetNextMedicineNotification(Patient patient)
        {
            return _entities.Where(pmn => pmn.PrescribedMedicine.MedicalRecord.Patient == patient && pmn.Triggered == false).OrderBy(pt => pt.TriggerTime).FirstOrDefault();
        }


    }
}
