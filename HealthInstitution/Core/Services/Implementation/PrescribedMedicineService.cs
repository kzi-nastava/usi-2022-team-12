using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Services.Interfaces;

namespace HealthInstitution.Core.Services.Implementation
{
    public class PrescribedMedicineService : CrudService<PrescribedMedicine>, IPrescribedMedicineService
    {
        public PrescribedMedicineService(DatabaseContext context) : base(context) { }
    }
}
