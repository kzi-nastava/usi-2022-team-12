using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Features.EquipmentManagement.Services;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Repository;
using HealthInstitution.Core.Persistence;

namespace HealthInstitution.Core.Features.RoomManagement.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IEquipmentService _equipmentService;

        public RoomService(IRoomRepository roomRepository, IEquipmentService equipmentService)
        {
            _roomRepository = roomRepository;
            _equipmentService = equipmentService;
        }

        public IEnumerable<Room> ReadRooms(RoomType rt)
        {
            return _roomRepository.ReadAll().Where(room => room.RoomType == rt)
                            .ToList();
        }

        public IEnumerable<Room> ReadRooms(List<RoomType> types)
        {
            return _roomRepository.ReadAll().Where(room => types.Contains(room.RoomType))
                            .ToList();
        }

        public IEnumerable<Room> ReadRoomsWithName(string name)
        {
            return _roomRepository.ReadAll().Where(room => room.Name == name).ToList();
        }


        public IEnumerable<Room> FilterRoomsLowOnEquipment(string searchText)
        {
            searchText = searchText.ToLower();
            return GetRoomsLowOnEquipment().Where(room => room.Name.ToLower().Contains(searchText) || room.RoomType.ToString().ToLower().Contains(searchText));
        }

        public IEnumerable<Room> GetRoomsLowOnEquipment()
        {
            var allRooms = _roomRepository.ReadAll();
            var priorityRooms = new List<Room>();
            var requiredEquipment = _equipmentService.GetEquipment(EquipmentType.DynamicEquipment);

            foreach (var room in allRooms)
            {
                if (IsRoomMissingEquipment(room, requiredEquipment))
                    priorityRooms.Add(room);
                else if (room.IsLowOnEquipment())
                    priorityRooms.Add(room);
            }

            return priorityRooms;
        }

        private bool IsRoomMissingEquipment(Room room, IEnumerable<Equipment> requiredEquipment)
        {
            var missingEquipment = room.GetMissingEquipment(new List<Equipment>(requiredEquipment));
            return missingEquipment.Count != 0;
        }

        public Room GetStorage()
        {
            return _roomRepository.ReadAll().FirstOrDefault(rm => rm.Name == "S");
        }

        public void AddItemQuantityToStorage(Entry<Equipment> deliveredEquipment)
        {
            var storage = GetStorage();
            AddItemQuantity(storage, deliveredEquipment);
        }

        public void AddItemQuantity(Room room, Entry<Equipment> deliveredEquipment)
        {
            IncreaseItemQuantity(room, deliveredEquipment);
            _roomRepository.Update(room);
        }

        public void IncreaseItemQuantity(Room room, Entry<Equipment> deliveredEquipment)
        {
            var entryToUpdate = room.Inventory.FirstOrDefault(e => e.Item.Id == deliveredEquipment.Item.Id);
            if (entryToUpdate != null)
                entryToUpdate.Quantity += deliveredEquipment.Quantity;
            else
                room.Inventory.Add(deliveredEquipment);
        }
    }
}
