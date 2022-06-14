using HealthInstitution.Model;
using HealthInstitution.Persistence;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Model.room;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.Services.Implementation
{
    public class EntryService : CrudService<Entry<Equipment>>, IEntryService
    {
        public EntryService(DatabaseContext context) : base(context)
        {

        }
    }
}
