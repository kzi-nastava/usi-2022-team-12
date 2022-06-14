using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.MedicalRecordManagement.Repository
{
    public class AllergenRepository : CrudRepository<Allergen>, IAllergenRepository
    {
        public AllergenRepository(DatabaseContext context) : base(context) { }
    }
}
