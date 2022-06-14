using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Services.Interfaces;

namespace HealthInstitution.Core.Services.Implementation
{
    public class EquipmentPurchaseRequestService : CrudService<EquipmentPurchaseRequest>, IEquipmentPurchaseRequestService
    {
        private readonly IRoomService _roomService;

        public EquipmentPurchaseRequestService(DatabaseContext context, RoomService roomService) : base(context)
        {
            _roomService = roomService;
        }

        public IList<EquipmentPurchaseRequest> GetPendingRequests()
        {
            return _entities.Where(r => r.DateOfTransfer < DateTime.Now)
                            .ToList();
        }

        public void UpdateEquipmentQuantity()
        {
            foreach (EquipmentPurchaseRequest purchaseRequest in GetPendingRequests())
            {
                _roomService.AddItemQuantityToStorage(purchaseRequest.PurchasedEquipment);

                purchaseRequest.IsDone = true;
                Update(purchaseRequest);
            }
        }
    }
}
