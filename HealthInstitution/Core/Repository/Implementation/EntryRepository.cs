using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository.Implementation
{
    public class EntryRepository : CrudRepository<Entry<Equipment>>, IEntryRepository
    {
        public EntryRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
