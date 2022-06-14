using HealthInstitution.Model;
using HealthInstitution.Persistence;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.Core.Features.EquipmentManagement.Model;

namespace HealthInstitution.Services.Implementation
{
    public class EntryService : CrudService<Entry<Equipment>>, IEntryService
    {
        public EntryService(DatabaseContext context) : base(context)
        {

        }
    }
}
