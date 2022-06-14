using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Services.Interfaces;

namespace HealthInstitution.Core.Services.Implementation
{
    public class EntryService : CrudService<Entry<Equipment>>, IEntryService
    {
        public EntryService(DatabaseContext context) : base(context)
        {

        }
    }
}
