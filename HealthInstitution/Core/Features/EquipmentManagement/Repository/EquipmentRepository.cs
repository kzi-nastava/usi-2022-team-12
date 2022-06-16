using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.EquipmentManagement.Repository
{
    public class EquipmentRepository : CrudRepository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
