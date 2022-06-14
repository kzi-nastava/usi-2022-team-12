using HealthInstitution.Core.Features.EquipmentManagement.Model;
using System.Collections.Generic;

namespace HealthInstitution.Core.Features.EquipmentManagement.Service
{
    public interface IEquipmentPurchaseRequestService
    {
        IList<EquipmentPurchaseRequest> GetPendingRequests();
        void UpdateEquipmentQuantity();
    }
}