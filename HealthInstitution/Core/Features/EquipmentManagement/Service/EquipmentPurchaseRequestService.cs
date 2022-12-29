using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Features.EquipmentManagement.Repository;
using HealthInstitution.Core.Features.RoomManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthInstitution.Core.Features.EquipmentManagement.Service
{
    public class EquipmentPurchaseRequestService : IEquipmentPurchaseRequestService
    {
        private readonly IEquipmentPurchaseRequestRepository _equipmentPurchaseRequestRepository;

        private readonly IRoomService _roomService;

        public EquipmentPurchaseRequestService(IEquipmentPurchaseRequestRepository equipmentPurchaseRequestRepository, IRoomService roomService)
        {
            _equipmentPurchaseRequestRepository = equipmentPurchaseRequestRepository;
            _roomService = roomService;
        }

        #region CRUD methods

        public IEnumerable<EquipmentPurchaseRequest> ReadAll()
        {
            return _equipmentPurchaseRequestRepository.ReadAll();
        }

        public EquipmentPurchaseRequest Read(Guid requestId)
        {
            return _equipmentPurchaseRequestRepository.Read(requestId);
        }

        public EquipmentPurchaseRequest Create(EquipmentPurchaseRequest newRequest)
        {
            return _equipmentPurchaseRequestRepository.Create(newRequest);
        }

        public EquipmentPurchaseRequest Update(EquipmentPurchaseRequest requestToUpdate)
        {
            return _equipmentPurchaseRequestRepository.Update(requestToUpdate);
        }

        public EquipmentPurchaseRequest Delete(Guid requestId)
        {
            return _equipmentPurchaseRequestRepository.Delete(requestId);
        }

        #endregion

        public IList<EquipmentPurchaseRequest> GetPendingRequests()
        {
            return _equipmentPurchaseRequestRepository.ReadAll().Where(r => r.DateOfTransfer < DateTime.Now)
                                                                            .Where(r => !r.IsDone)
                                                                            .ToList();
        }

        public void UpdateEquipmentQuantity()
        {
            foreach (EquipmentPurchaseRequest purchaseRequest in GetPendingRequests())
            {
                _roomService.AddItemQuantityToStorage(purchaseRequest.PurchasedEquipment);

                purchaseRequest.IsDone = true;
                _equipmentPurchaseRequestRepository.Update(purchaseRequest);
            }
        }
    }
}
