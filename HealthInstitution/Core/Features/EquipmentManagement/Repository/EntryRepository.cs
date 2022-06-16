using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.EquipmentManagement.Repository
{
    public class EntryRepository : CrudRepository<Entry<Equipment>>, IEntryRepository
    {
        public EntryRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
