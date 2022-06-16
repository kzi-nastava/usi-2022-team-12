using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.MedicineManagement.Repository
{
    public class MedicineRepository : CrudRepository<Medicine>, IMedicineRepository
    {
        public MedicineRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
