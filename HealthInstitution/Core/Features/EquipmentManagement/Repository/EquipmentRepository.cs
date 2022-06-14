using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.EquipmentManagement.Repository
{
    public class EquipmentRepository : CrudRepository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
