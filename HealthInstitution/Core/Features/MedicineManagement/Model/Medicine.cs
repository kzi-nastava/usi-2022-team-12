using HealthInstitution.Model;
using System.Collections.Generic;

namespace HealthInstitution.Core.Features.MedicineManagement.Model
{
    public class Medicine : BaseObservableEntity
    {
        #region Attributes

        private IList<Ingredient> _ingredients;
        public virtual IList<Ingredient> Ingredients { get => _ingredients; set => OnPropertyChanged(ref _ingredients, value); }

        private string _description;
        public string Description { get => _description; set => OnPropertyChanged(ref _description, value); }

        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        private Status _status;
        public Status Status { get => _status; set => OnPropertyChanged(ref _status, value); }

        #endregion

        #region Constructor

        public Medicine()
        {
            _ingredients = new List<Ingredient>();
        }

        /*public Medicine(string description)
        {
            _ingredients = new List<Ingredient>();
            _description = description;
            _status = Status.Approved;
        }*/

        #endregion

        #region Methods

        public void AddIngredient(Ingredient ingredient)
        {
            foreach (Ingredient ingredientInMedicine in _ingredients)
            {
                if (ingredientInMedicine.Id == ingredient.Id)
                {
                    // Baci exception
                    return;
                }
            }

            _ingredients.Add(ingredient);
        }

        #endregion
    }
}
