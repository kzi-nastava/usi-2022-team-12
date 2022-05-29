using HealthInstitution.Model;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Services.Intefaces
{
    public interface INotificationService : ICrudService<Notification>
    {
        public IList<Notification> GetValidNotificationsForUser(Guid userId);
        public IEnumerable<PrescribedMedicine> GetConsumingMedications(Patient patient, int notifyMinutesBefore);
        public List<PrescribedMedicine> FilterNextMedications(Patient patient, int notifyMinutesBefore);
    }
}
