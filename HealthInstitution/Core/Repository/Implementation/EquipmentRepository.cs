using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository.Implementation
{
    public class EquipmentRepository : CrudRepository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
