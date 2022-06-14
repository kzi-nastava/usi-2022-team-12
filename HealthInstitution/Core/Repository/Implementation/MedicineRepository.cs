using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository.Implementation
{
    public class MedicineRepository : CrudRepository<Medicine>, IMedicineRepository
    {
        public MedicineRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
