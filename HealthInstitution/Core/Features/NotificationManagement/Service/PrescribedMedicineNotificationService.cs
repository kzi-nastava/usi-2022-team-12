using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Features.NotificationManagement.Model;
using HealthInstitution.Core.Features.NotificationManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.MedicineManagement.Repository;
using HealthInstitution.Core.Persistence;

namespace HealthInstitution.Core.Features.NotificationManagement.Service
{
    public class PrescribedMedicineNotificationService : IPrescribedMedicineNotificationService
    {
        private readonly IPrescribedMedicineNotificationRepository _prescribedMedicineNotificationRepository;
        private readonly IPrescribedMedicineRepository _prescribedMedicineRepository;
        public PrescribedMedicineNotificationService(IPrescribedMedicineNotificationRepository prescribedMedicineNotificationRepository, IPrescribedMedicineRepository prescribedMedicineRepository)
        {
            _prescribedMedicineNotificationRepository = prescribedMedicineNotificationRepository;
            _prescribedMedicineRepository = prescribedMedicineRepository;
        }

        public PrescribedMedicineNotificationService(DatabaseContext databaseContext) {
            _prescribedMedicineNotificationRepository = new PrescribedMedicineNotificationRepository(databaseContext);
            _prescribedMedicineRepository = new PrescribedMedicineRepository(databaseContext);
        }

        #region CRUD methods

        public IEnumerable<PrescribedMedicineNotification> ReadAll()
        {
            return _prescribedMedicineNotificationRepository.ReadAll();
        }

        public PrescribedMedicineNotification Read(Guid prescribedMedicineNotificationId)
        {
            return _prescribedMedicineNotificationRepository.Read(prescribedMedicineNotificationId);
        }

        public PrescribedMedicineNotification Create(PrescribedMedicineNotification newPrescribedMedicineNotification)
        {
            return _prescribedMedicineNotificationRepository.Create(newPrescribedMedicineNotification);
        }

        public PrescribedMedicineNotification Update(PrescribedMedicineNotification prescribedMedicineNotificationToUpdate)
        {
            return _prescribedMedicineNotificationRepository.Update(prescribedMedicineNotificationToUpdate);
        }

        public PrescribedMedicineNotification Delete(Guid prescribedMedicineNotificationId)
        {
            return _prescribedMedicineNotificationRepository.Delete(prescribedMedicineNotificationId);
        }

        #endregion

        public IEnumerable<PrescribedMedicine> GetConsumingMedications(Patient patient, int notifyMinutesBefore)
        {
            return _prescribedMedicineRepository.ReadAll().Where(pm => pm.MedicalRecord.Patient.Id == patient.Id && pm.UsageStart.AddMinutes(-notifyMinutesBefore) < DateTime.Now && pm.UsageEnd > DateTime.Now);
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
                if (_prescribedMedicineNotificationRepository.ReadAll().Where(pmn => pmn.PrescribedMedicine.Id == upcomingMedicineNotification.PrescribedMedicine.Id && pmn.TriggerTime == upcomingMedicineNotification.TriggerTime).ToList().Count == 0)
                {
                    _prescribedMedicineNotificationRepository.Create(upcomingMedicineNotification);
                }
            }
        }
        public IEnumerable<PrescribedMedicineNotification> GetUpcomingMedicinesNotifications(Patient patient)
        {
            return _prescribedMedicineNotificationRepository.ReadAll().Where(pmn => pmn.PrescribedMedicine.MedicalRecord.Patient.Id == patient.Id && pmn.Triggered == false);
        }

        public void DeleteUpcomingMedicinesNotifications(Patient patient)
        {
            var notificationsToRemove = GetUpcomingMedicinesNotifications(patient);
            foreach (var notification in notificationsToRemove)
            {
                _prescribedMedicineNotificationRepository.Delete(notification.Id);
            }
        }
        public void DeleteAllUpcomingMedicinesNotifications()
        {
            var notificationsToRemove = _prescribedMedicineNotificationRepository.ReadAll().Where(pmn => pmn.Triggered == false);
            foreach (var notification in notificationsToRemove)
            {
                _prescribedMedicineNotificationRepository.Delete(notification.Id);
            }
        }

        public PrescribedMedicineNotification GetNextMedicineNotification(Patient patient)
        {
            return _prescribedMedicineNotificationRepository.ReadAll().Where(pmn => pmn.PrescribedMedicine.MedicalRecord.Patient.Id == patient.Id && pmn.Triggered == false).OrderBy(pt => pt.TriggerTime).FirstOrDefault();
        }
    }
}
