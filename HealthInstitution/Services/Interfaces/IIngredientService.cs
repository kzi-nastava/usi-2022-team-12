using HealthInstitution.Model.medicine;

namespace HealthInstitution.Services.Interfaces
{
    public interface IIngredientService : ICrudService<Ingredient>
    {
        public bool IngredientExists(string name);
    }
}
