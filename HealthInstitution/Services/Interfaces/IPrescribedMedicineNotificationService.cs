using System.Collections.Generic;
using HealthInstitution.Model.doctor;
using HealthInstitution.Model.user;

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