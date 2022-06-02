using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Model.room;

namespace HealthInstitution.Services.Implementation
{
    public class EntryService : CrudService<Entry<Equipment>>, IEntryService
    {
        public EntryService(DatabaseContext context) : base(context)
        {

        }
    }
}
