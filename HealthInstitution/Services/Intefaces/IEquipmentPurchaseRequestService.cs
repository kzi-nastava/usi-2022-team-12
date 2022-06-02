using HealthInstitution.Model;

namespace HealthInstitution.Services.Intefaces
{
    public interface IEquipmentPurchaseRequestService : ICrudService<EquipmentPurchaseRequest>
    {
        public void UpdateEquipmentQuantity();
    }
}
