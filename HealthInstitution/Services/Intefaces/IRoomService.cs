﻿using HealthInstitution.Model;
using System.Collections.Generic;
using HealthInstitution.Model.room;

namespace HealthInstitution.Services.Intefaces
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

    }
}
