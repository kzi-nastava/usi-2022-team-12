using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Persistence;
using HealthInstitution.Core.Repository.Interfaces;
using HealthInstitution.Core.Features.NotificationManagement.Model;

namespace HealthInstitution.Core.Repository.Implementation
{
    public class PrescribedMedicineNotificationRepository : CrudRepository<PrescribedMedicineNotification>, IPrescribedMedicineNotificationRepository
    {
        public PrescribedMedicineNotificationRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
