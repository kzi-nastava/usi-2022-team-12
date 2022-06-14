using HealthInstitution.Core.Features.EquipmentManagement.Model;

namespace HealthInstitution.Core.Services.Interfaces
{
    public interface IEquipmentPurchaseRequestService : ICrudService<EquipmentPurchaseRequest>
    {
        public void UpdateEquipmentQuantity();
    }
}
