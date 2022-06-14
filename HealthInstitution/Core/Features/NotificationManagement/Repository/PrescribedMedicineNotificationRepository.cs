using HealthInstitution.Core.Features.NotificationManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.NotificationManagement.Repository
{
    public class PrescribedMedicineNotificationRepository : CrudRepository<PrescribedMedicineNotification>, IPrescribedMedicineNotificationRepository
    {
        public PrescribedMedicineNotificationRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
