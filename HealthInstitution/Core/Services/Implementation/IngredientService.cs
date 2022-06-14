using System.Linq;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Services.Interfaces;

namespace HealthInstitution.Core.Services.Implementation
{
    public class IngredientService : CrudService<Ingredient>, IIngredientService
    {
        public IngredientService(DatabaseContext context) : base(context)
        {

        }

        public bool IngredientExists(string name)
        {
            return _entities.Where(i => i.Name.ToLower() == name.ToLower()).Count() != 0;
        }

    }
}