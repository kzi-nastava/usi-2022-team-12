using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Model.doctor;

namespace HealthInstitution.Services.Implementation
{
    public class PrescribedMedicineService : CrudService<PrescribedMedicine>, IPrescribedMedicineService
    {
        public PrescribedMedicineService(DatabaseContext context) : base(context) { }
    }
}
