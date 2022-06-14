using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Persistence;
using HealthInstitution.Core.Features.NotificationManagement.Model;
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
