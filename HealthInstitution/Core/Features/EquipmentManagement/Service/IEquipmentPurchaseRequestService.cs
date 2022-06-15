using HealthInstitution.Core.Features.EquipmentManagement.Model;
using System.Collections.Generic;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.EquipmentManagement.Service
{
    public interface IEquipmentPurchaseRequestService : ICrudService<EquipmentPurchaseRequest>
    {
        IList<EquipmentPurchaseRequest> GetPendingRequests();
        void UpdateEquipmentQuantity();
    }
}