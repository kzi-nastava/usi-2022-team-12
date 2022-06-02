using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System.Collections.Generic;
using System.Linq;

namespace HealthInstitution.Services.Implementation
{
    public class EquipmentService : CrudService<Equipment>, IEquipmentService
    {
        private readonly IRoomService _roomService;

        public EquipmentService(DatabaseContext context, IRoomService roomService) : base(context)
        {
            _roomService = roomService;
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
