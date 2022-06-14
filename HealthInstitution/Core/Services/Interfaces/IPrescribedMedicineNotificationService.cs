using System.Collections.Generic;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Features.NotificationManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Services.Interfaces
{
    public interface IPrescribedMedicineNotificationService : ICrudService<PrescribedMedicineNotification>
    {
        public IEnumerable<PrescribedMedicine> GetConsumingMedications(Patient patient, int notifyMinutesBefore);
        public List<PrescribedMedicineNotification> GenerateUpcomingMedicinesNotifications(Patient patient, int notifyMinutesBefore);
        public void CreateUpcomingMedicinesNotifications(Patient patient, int notifyMinutesBefore);
        public void DeleteUpcomingMedicinesNotifications(Patient patient);
        public void DeleteAllUpcomingMedicinesNotifications();
        public PrescribedMedicineNotification GetNextMedicineNotification(Patient patient);

    }
}