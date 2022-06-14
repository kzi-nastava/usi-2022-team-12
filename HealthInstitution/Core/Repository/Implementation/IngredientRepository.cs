using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository.Implementation
{
    public class IngredientRepository : CrudRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
