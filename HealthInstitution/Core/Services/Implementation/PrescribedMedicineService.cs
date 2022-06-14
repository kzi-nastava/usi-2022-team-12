using HealthInstitution.Model;
using HealthInstitution.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.Core.Features.MedicineManagement.Model;

namespace HealthInstitution.Services.Implementation
{
    public class PrescribedMedicineService : CrudService<PrescribedMedicine>, IPrescribedMedicineService
    {
        public PrescribedMedicineService(DatabaseContext context) : base(context) { }
    }
}
