using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository.Implementation
{
    public class IllnessRepository : CrudRepository<Illness>, IIllnessRepository
    {
        public IllnessRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
}
