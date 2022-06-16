using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;
using System.Linq;

namespace HealthInstitution.Core.Features.MedicineManagement.Repository
{
    public class IngredientRepository : CrudRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(DatabaseContext context) : base(context)
        {

        }

        public bool IngredientExists(string name)
        {
            return _entities.Where(i => i.Name.ToLower() == name.ToLower()).Count() != 0;
        }
    }
}
