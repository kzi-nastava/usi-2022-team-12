using HealthInstitution.Model;
using System.Collections.Generic;

namespace HealthInstitution.Services.Intefaces
{
    public interface IEquipmentService : ICrudService<Equipment>
    {
        public IEnumerable<Equipment> GetEquipment(EquipmentType resuestedType);

        public IEnumerable<Equipment> FilterEquipmentBySearchText(EquipmentType resuestedType, string searchText);
    }
}
