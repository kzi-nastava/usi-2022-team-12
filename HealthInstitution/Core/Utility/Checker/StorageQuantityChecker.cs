using System.Threading;
using HealthInstitution.Core.Features.EquipmentManagement.Repository;
using HealthInstitution.Core.Features.EquipmentManagement.Service;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.Core.Ninject;
using HealthInstitution.Core.Persistence;

namespace HealthInstitution.Core.Utility.Checker
{
    public class StorageQuantityChecker
    {
        private static Timer _timer;
        private static readonly IEquipmentPurchaseRequestService _equipmentPurchaseRequestService;

        static StorageQuantityChecker()
        {
            _equipmentPurchaseRequestService = new EquipmentPurchaseRequestService(new EquipmentPurchaseRequestRepository(new DatabaseContext(0)), ServiceLocator.Get<RoomService>());
        }
        public static void InitializeTimer()
        {
            StopTimer();

            _timer = new Timer(UpdateStorage, null, 1000, 60000);
        }

        public static void StopTimer()
        {
            if (_timer != null)
            {
                _timer.Dispose();
            }
        }

        public static void UpdateStorage(object stateInfo)
        {
            _equipmentPurchaseRequestService.UpdateEquipmentQuantity();
        }
    }
}
