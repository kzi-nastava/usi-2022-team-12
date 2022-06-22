using System.Threading;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.EquipmentManagement.Repository;
using HealthInstitution.Core.Features.EquipmentManagement.Service;
using HealthInstitution.Core.Features.RoomManagement.Repository;
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
            var equipmentService = ServiceLocator.Get<EquipmentService>();
            var roomRenovationRepository = ServiceLocator.Get<RoomRenovationRepository>();
            var appointmentUpdateRequestRepository = ServiceLocator.Get<AppointmentUpdateRequestRepository>();
            var appointmentRepository = ServiceLocator.Get<AppointmentRepository>();

            var roomService = new RoomService(new RoomRepository(new DatabaseContext(0)), equipmentService, roomRenovationRepository, appointmentUpdateRequestRepository, appointmentRepository);
            _equipmentPurchaseRequestService = new EquipmentPurchaseRequestService(new EquipmentPurchaseRequestRepository(new DatabaseContext(0)), roomService);
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
