using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;

namespace HealthInstitution.Services.Implementation
{
    public class EquipmentPurchaseRequestService : CrudService<EquipmentPurchaseRequest>, IEquipmentPurchaseRequestService
    {
        public EquipmentPurchaseRequestService(DatabaseContext context) : base(context)
        {
        }
    }
}
