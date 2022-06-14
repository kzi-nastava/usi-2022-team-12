using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.MedicineManagement.Repository
{
    public class IngredientRepository : CrudRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
