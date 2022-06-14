using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository.Interfaces
{
    internal interface IEntryRepository : ICrudRepository<Entry<Equipment>>
    {
    }
}
