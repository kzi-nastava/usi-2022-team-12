using HealthInstitution.Core.Features.MedicineManagement.Model;

namespace HealthInstitution.Services.Interfaces
{
    public interface IIngredientService : ICrudService<Ingredient>
    {
        public bool IngredientExists(string name);
    }
}
