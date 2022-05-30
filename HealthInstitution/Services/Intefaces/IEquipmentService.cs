using HealthInstitution.Model;
using System.Collections.Generic;

namespace HealthInstitution.Services.Intefaces
{
    public interface IEquipmentService : ICrudService<Equipment>
    {
        public IEnumerable<Equipment> GetEquipment(EquipmentType requestedType);

        public IEnumerable<Equipment> FilterEquipmentNotInRoomBySearchText(Room room, EquipmentType requestedType, string searchText);

        public IEnumerable<Equipment> GetEquipmentNotInRoom(Room room, EquipmentType requestType);
    }
}
