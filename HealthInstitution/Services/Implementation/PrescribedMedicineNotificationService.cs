using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<PrescribedMedicineNotification> GetUpcomingMedicationsNotifications(Patient patient, int notifyMinutesBefore)
        {
            List<PrescribedMedicineNotification> upcomingMedactionsNotifications = new List<PrescribedMedicineNotification>();
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
                    upcomingMedactionsNotifications.Add(new PrescribedMedicineNotification { PrescribedMedicine = prescribedMedicine, TriggerTime = nextTaking, Triggered = false });
                }
            }
            return upcomingMedactionsNotifications;
        }

        public void CreateUpcomingMedicationsNotifications(Patient patient, int notifyMinutesBefore)
        {
            var upcomingMedicinesNotifications = GetUpcomingMedicationsNotifications(patient, notifyMinutesBefore);
            foreach (var upcomingMedicineNotification in upcomingMedicinesNotifications)
            {
                if (_entities.Where(pmn => pmn.PrescribedMedicine == upcomingMedicineNotification.PrescribedMedicine && pmn.TriggerTime == upcomingMedicineNotification.TriggerTime).ToList().Count == 0){
                    Create(upcomingMedicineNotification);
                }
            }
        }

        public PrescribedMedicineNotification GetNextMedicineNotification(Patient patient)
        {
            return _entities.Where(pmn => pmn.PrescribedMedicine.MedicalRecord.Patient == patient && pmn.Triggered == false).OrderBy(pt => pt.TriggerTime).FirstOrDefault();
        }
    }
}
