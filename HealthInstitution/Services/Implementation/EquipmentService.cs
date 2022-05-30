using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Equipment> FilterEquipmentBySearchText(EquipmentType requestType, string searchText)
        {
            searchText = searchText.ToLower();
            return _entities.Where(e => e.EquipmentType == requestType)
                            .Where(e => e.Name.ToLower().Contains(searchText))
                            .ToList();
        }
    }
}
