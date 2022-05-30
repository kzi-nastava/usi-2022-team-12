using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthInstitution.Services.Implementation
{
    public class NotificationService : CrudService<Notification>, INotificationService
    {
        public NotificationService(DatabaseContext context) : base(context)
        {
        }

        public IList<Notification> GetValidNotificationsForUser(Guid userId)
        {
            return _entities.Where(n => n.IsShown == false)
                            .Where(n => n.UserId == userId)
                            .ToList();
        }

        public IEnumerable<PrescribedMedicine> GetConsumingMedications(Patient patient, int notifyMinutesBefore) {
            return _context.Set<PrescribedMedicine>().Where(pm => pm.MedicalRecord.Patient == patient && pm.UsageStart.AddMinutes(-notifyMinutesBefore) < DateTime.Now && pm.UsageEnd > DateTime.Now);
        }

        public List<PrescribedMedicine> FilterNextMedications(Patient patient, int notifyMinutesBefore) {
            List<PrescribedMedicine> upcomingMedactions = new List<PrescribedMedicine>();
            var currentConsumingMedications = GetConsumingMedications(patient, notifyMinutesBefore).ToList();
            foreach (var medication in currentConsumingMedications)
            {
                var nextTaking = medication.UsageStart;
                while (nextTaking < DateTime.Now) {
                    nextTaking = medication.UsageStart.AddHours(medication.UsageHourSpan);
                }
                if (DateTime.Now.AddMinutes(-notifyMinutesBefore) < nextTaking) {
                    upcomingMedactions.Add(medication);
                }
            }
            return upcomingMedactions;
        }
    }
}
