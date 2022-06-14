using HealthInstitution.Model;
using HealthInstitution.Persistence;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;

namespace HealthInstitution.Services.Implementation
{
    public class EquipmentService : CrudService<Equipment>, IEquipmentService
    {
        public EquipmentService(DatabaseContext context) : base(context)
        {
        }

        public IEnumerable<Equipment> GetEquipment(EquipmentType requestType)
        {
            return _entities.Where(e => e.EquipmentType == requestType).ToList();
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

            foreach (Entry<Equipment> equipmentEntry in room.Inventory)
            {
                if (equipmentEntry.Quantity == 0)
                    unavailableEquipment.Add(equipmentEntry.Item);
            }

            foreach (Equipment equipment in equipmentForType)
            {
                if (!room.ContainsEquipment(equipment))
                    unavailableEquipment.Add(equipment);
            }

            return unavailableEquipment;
        }
    }
}
