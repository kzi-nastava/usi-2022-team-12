using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Features.NotificationManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Features.NotificationManagement.Service
{
    public interface IPrescribedMedicineNotificationService
    {
        PrescribedMedicineNotification Create(PrescribedMedicineNotification newPrescribedMedicineNotification);
        void CreateUpcomingMedicinesNotifications(Patient patient, int notifyMinutesBefore);
        PrescribedMedicineNotification Delete(Guid prescribedMedicineNotificationId);
        void DeleteAllUpcomingMedicinesNotifications();
        void DeleteUpcomingMedicinesNotifications(Patient patient);
        List<PrescribedMedicineNotification> GenerateUpcomingMedicinesNotifications(Patient patient, int notifyMinutesBefore);
        IEnumerable<PrescribedMedicine> GetConsumingMedications(Patient patient, int notifyMinutesBefore);
        PrescribedMedicineNotification GetNextMedicineNotification(Patient patient);
        IEnumerable<PrescribedMedicineNotification> GetUpcomingMedicinesNotifications(Patient patient);
        PrescribedMedicineNotification Read(Guid prescribedMedicineNotificationId);
        IEnumerable<PrescribedMedicineNotification> ReadAll();
        PrescribedMedicineNotification Update(PrescribedMedicineNotification prescribedMedicineNotificationToUpdate);
    }
}