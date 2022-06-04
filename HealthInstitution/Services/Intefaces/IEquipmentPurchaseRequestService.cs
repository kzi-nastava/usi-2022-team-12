using HealthInstitution.Model;
using HealthInstitution.Model.room;

namespace HealthInstitution.Services.Intefaces
{
    public interface IEquipmentPurchaseRequestService : ICrudService<EquipmentPurchaseRequest>
    {
        public void UpdateEquipmentQuantity();
    }
}
