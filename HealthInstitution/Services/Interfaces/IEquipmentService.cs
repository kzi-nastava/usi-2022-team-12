using System.Collections.Generic;
using HealthInstitution.Model.room;

namespace HealthInstitution.Services.Interfaces
{
    public interface IEquipmentService : ICrudService<Equipment>
    {
        public IEnumerable<Equipment> GetEquipment(EquipmentType requestedType);

        public IEnumerable<Equipment> FilterEquipmentNotInRoomBySearchText(Room room, EquipmentType requestedType, string searchText);

        public IEnumerable<Equipment> GetEquipmentNotInRoom(Room room, EquipmentType requestType);

    }
}
