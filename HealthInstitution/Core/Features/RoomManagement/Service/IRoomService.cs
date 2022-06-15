using System;
using System.Collections.Generic;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.RoomManagement.Service
{
    public interface IRoomService : ICrudService<Room>
    {
        public IEnumerable<Room> ReadRooms(RoomType rt);

        public IEnumerable<Room> ReadRooms(List<RoomType> types);

        IEnumerable<Room> ReadRoomsWithName(string name);

        public IEnumerable<Room> FilterRoomsLowOnEquipment(string searchText);

        public IEnumerable<Room> GetRoomsLowOnEquipment();

        public Room GetStorage();

        public void AddItemQuantityToStorage(Entry<Equipment> deliveredEquipment);

        public void AddItemQuantity(Room room, Entry<Equipment> deliveredEquipment);

        public void IncreaseItemQuantity(Room room, Entry<Equipment> deliveredEquipment);

        Room FindFreeRoom(RoomType roomType, DateTime start, DateTime end);

        bool IsRoomAvailableForUpdate(Room room, DateTime fromDate, DateTime toDate, Appointment aptToUpdate);

        bool IsRoomAvailable(Room room, DateTime fromDate, DateTime toDate);
    }
}
