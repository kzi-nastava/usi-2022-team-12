using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.EquipmentManagement.Repository
{
    public class EquipmentPurchaseRequestRepository : CrudRepository<EquipmentPurchaseRequest>, IEquipmentPurchaseRequestRepository
    {
        public EquipmentPurchaseRequestRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
