using HealthInstitution.Model;
using System.Collections.Generic;
using HealthInstitution.Model.medicine;

namespace HealthInstitution.Services.Intefaces
{
    public interface IIngredientService : ICrudService<Ingredient>
    {
        public bool IngredientExists(string name);
    }
}
