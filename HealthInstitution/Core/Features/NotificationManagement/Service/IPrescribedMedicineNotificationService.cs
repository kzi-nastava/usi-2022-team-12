using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Features.NotificationManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using System.Collections.Generic;

namespace HealthInstitution.Core.Features.NotificationManagement.Service
{
    public interface IPrescribedMedicineNotificationService
    {
        void CreateUpcomingMedicinesNotifications(Patient patient, int notifyMinutesBefore);
        void DeleteAllUpcomingMedicinesNotifications();
        void DeleteUpcomingMedicinesNotifications(Patient patient);
        List<PrescribedMedicineNotification> GenerateUpcomingMedicinesNotifications(Patient patient, int notifyMinutesBefore);
        IEnumerable<PrescribedMedicine> GetConsumingMedications(Patient patient, int notifyMinutesBefore);
        PrescribedMedicineNotification GetNextMedicineNotification(Patient patient);
        IEnumerable<PrescribedMedicineNotification> GetUpcomingMedicinesNotifications(Patient patient);
    }
}