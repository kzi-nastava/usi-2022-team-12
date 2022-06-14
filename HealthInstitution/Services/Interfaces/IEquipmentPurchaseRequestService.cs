using HealthInstitution.Model.room;

namespace HealthInstitution.Services.Interfaces
{
    public interface IEquipmentPurchaseRequestService : ICrudService<EquipmentPurchaseRequest>
    {
        public void UpdateEquipmentQuantity();
    }
}
