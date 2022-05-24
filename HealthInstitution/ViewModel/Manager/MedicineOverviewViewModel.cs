using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class MedicineOverviewViewModel : ViewModelBase
    {
        public readonly IMedicineService medicineService;
        public readonly IIngredientService ingredientService;

        private List<Medicine> _allMedicine;

        public List<Medicine> AllMedicine
        {
            get => _allMedicine;
            set
            {
                _allMedicine = value;
                OnPropertyChanged(nameof(AllMedicine));
            }
        }

        private List<Ingredient> _allIngredients;

        public List<Ingredient> AllIngredients
        {
            get => _allIngredients;
            set
            {
                _allIngredients = value;
                OnPropertyChanged(nameof(AllIngredients));
            }
        }

        private Ingredient _selectedIngredient;
        public Ingredient SelectedIngredient
        {
            get => _selectedIngredient;
            set
            {
                _selectedIngredient = value;
                bool b = false;
                if (NewIngredients.Count != 0)
                {
                    foreach (Ingredient ing in NewIngredients)
                    {
                        if (ing.Name == value.Name)
                        {
                            b = true;
                            break;
                        }
                    }
                    if (!b)
                    {
                        this.NewIngredients.Add(value);
                    }
                }
                OnPropertyChanged(nameof(SelectedIngredient));  
            }
        }

        private List<Ingredient> _newIngredients;

        public List<Ingredient> NewIngredients
        {
            get => _newIngredients;
            set
            {
                _newIngredients = value;
                OnPropertyChanged(nameof(NewIngredients));
            }
        }

        public MedicineOverviewViewModel(IMedicineService medicineService, IIngredientService ingredientService)
        {
            medicineService = medicineService;
            ingredientService = ingredientService;
            AllMedicine = medicineService.ReadAll().ToList();
            AllMedicine = AllMedicine.OrderBy(x => x.Name).ToList();
            AllIngredients = ingredientService.ReadAll().ToList();
            AllIngredients = AllIngredients.OrderBy(x => x.Name).ToList();
            NewIngredients = new List<Ingredient>();
        }
    }
}
