﻿using System;
using System.Threading;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Implementation;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.Utility
{
    public class StorageQuantityChecker
    {
        private static Timer _timer;
        private static readonly IEquipmentPurchaseRequestService _equipmentPurchaseRequestService;

        static StorageQuantityChecker()
        {
            _equipmentPurchaseRequestService = new EquipmentPurchaseRequestService(new DatabaseContext(0),
                                                                                    new RoomService(new DatabaseContext(0),
                                                                                    new EquipmentService(new DatabaseContext(0))));
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

        public static void UpdateStorage(Object stateInfo)
        {
            _equipmentPurchaseRequestService.UpdateEquipmentQuantity();
        }
    }
}
