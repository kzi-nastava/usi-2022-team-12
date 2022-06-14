using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Persistence;
using HealthInstitution.Core.Repository.Interfaces;
using HealthInstitution.Core.Features.MedicineManagement.Model;

namespace HealthInstitution.Core.Repository.Implementation
{
    public class PrescribedMedicineRepository : CrudRepository<PrescribedMedicine>, IPrescribedMedicineRepository
    {
        public PrescribedMedicineRepository(DatabaseContext context) : base(context) { }
    }
}
}
