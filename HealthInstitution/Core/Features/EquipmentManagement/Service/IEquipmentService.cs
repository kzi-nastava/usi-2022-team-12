using System.Collections.Generic;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;

namespace HealthInstitution.Core.Features.EquipmentManagement.Service
{
    public interface IEquipmentService
    {
        public IEnumerable<Equipment> GetEquipment(EquipmentType requestedType);

        public IEnumerable<Equipment> FilterEquipmentNotInRoomBySearchText(Room room, EquipmentType requestedType, string searchText);

        public IEnumerable<Equipment> GetEquipmentNotInRoom(Room room, EquipmentType requestType);

    }
}
