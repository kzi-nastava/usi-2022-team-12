using HealthInstitution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services.Intefaces
{
    public interface IPrescribedMedicineNotificationService : ICrudService<PrescribedMedicineNotification>
    {
        public IEnumerable<PrescribedMedicine> GetConsumingMedications(Patient patient, int notifyMinutesBefore);
        public List<PrescribedMedicineNotification> GetUpcomingMedicationsNotifications(Patient patient, int notifyMinutesBefore);
        public void CreateUpcomingMedicationsNotifications(Patient patient, int notifyMinutesBefore);
        public PrescribedMedicineNotification GetNextMedicineNotification(Patient patient);

    }
}