using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System.Collections.Generic;
using System.Linq;

namespace HealthInstitution.Services.Implementation
{
    public class RoomService : CrudService<Room>, IRoomService
    {
        public RoomService(DatabaseContext context) : base(context)
        {
        }

        public IEnumerable<Room> ReadRoomsWithType(RoomType rt)
        {
            return _entities.Where(rm => rm.RoomType == rt).ToList();
        }

        public IEnumerable<Room> ReadRoomsWithName(string name)
        {
            return _entities.Where(rm => rm.Name == name).ToList();
        }

        public Room GetStorage()
        {
            return _entities.FirstOrDefault(rm => rm.Name == "S");
        }

        public void AddItemQuantityToStorage(Entry<Equipment> deliveredEquipment)
        {
            Room storage = GetStorage();
            AddItemQuantity(storage, deliveredEquipment);
        }

        public void AddItemQuantity(Room room, Entry<Equipment> deliveredEquipment)
        {
            foreach (Entry<Equipment> equipment in room.Inventory)
            {
                if (!equipment.Item.Name.Equals(deliveredEquipment.Item.Name)) continue;

                equipment.Quantity += deliveredEquipment.Quantity;
                Update(room);
                return;
            }
        }
    }
}
