using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.EquipmentManagement.Repository
{
    public class EquipmentPurchaseRequestRepository : CrudRepository<EquipmentPurchaseRequest>, IEquipmentPurchaseRequestRepository
    {
        public EquipmentPurchaseRequestRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
