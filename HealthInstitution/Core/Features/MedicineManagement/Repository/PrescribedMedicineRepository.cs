using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.MedicineManagement.Repository
{
    public class PrescribedMedicineRepository : CrudRepository<PrescribedMedicine>, IPrescribedMedicineRepository
    {
        public PrescribedMedicineRepository(DatabaseContext context) : base(context) { }
    }
}
}
