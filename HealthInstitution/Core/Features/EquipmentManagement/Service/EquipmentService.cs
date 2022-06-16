using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Features.EquipmentManagement.Repository;
using HealthInstitution.Core.Features.RoomManagement.Model;

namespace HealthInstitution.Core.Features.EquipmentManagement.Service
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public EquipmentService(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        #region CRUD methods

        public IEnumerable<Equipment> ReadAll()
        {
            return _equipmentRepository.ReadAll();
        }

        public Equipment Read(Guid equipmentId)
        {
            return _equipmentRepository.Read(equipmentId);
        }

        public Equipment Create(Equipment newEquipment)
        {
            return _equipmentRepository.Create(newEquipment);
        }

        public Equipment Update(Equipment equipmentToUpdate)
        {
            return _equipmentRepository.Update(equipmentToUpdate);
        }

        public Equipment Delete(Guid requestId)
        {
            return _equipmentRepository.Delete(requestId);
        }

        #endregion

        public IEnumerable<Equipment> GetEquipment(EquipmentType requestType)
        {
            return _equipmentRepository.ReadAll().Where(e => e.EquipmentType == requestType).ToList();
        }
        public IEnumerable<Equipment> FilterEquipmentNotInRoomBySearchText(Room room, EquipmentType requestType, string searchText)
        {
            searchText = searchText.ToLower();
            return GetEquipmentNotInRoom(room, requestType)
                .Where(e => e.Name.ToLower().Contains(searchText))
                .ToList();
        }

        public IEnumerable<Equipment> GetEquipmentNotInRoom(Room room, EquipmentType requestType)
        {
            IList<Equipment> equipmentForType = new List<Equipment>(GetEquipment(requestType));
            IList<Equipment> unavailableEquipment = new List<Equipment>();

            for (var index = 0; index < room.Inventory.Count; index++)
            {
                var equipmentEntry = room.Inventory[index];
                if (equipmentEntry.Quantity == 0)
                    unavailableEquipment.Add(equipmentEntry.Item);
            }

            foreach (var equipment in equipmentForType)
            {
                if (!room.ContainsEquipment(equipment))
                    unavailableEquipment.Add(equipment);
            }

            return unavailableEquipment;
        }
    }
}
