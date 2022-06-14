using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Commands.manager;
using HealthInstitution.Model.medicine;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.ViewModel.manager
{
    public class IngredientOverviewViewModel : ViewModelBase
    {
        #region Attributes

        private IIngredientService _ingredientService;
        private IMedicineService _medicineService;
        private Ingredient _selectedIngredient;
        private string _nameBox;

        #endregion

        #region Properties

        public IIngredientService IngredientService
        {
            get => _ingredientService;
        }
        public IMedicineService MedicineService
        {
            get => _medicineService;
        }
        public Ingredient SelectedIngredient
        {
            get => _selectedIngredient;
            set
            {
                _selectedIngredient = value;
                OnPropertyChanged(nameof(SelectedIngredient));
                NameBox = value.Name;
                OnPropertyChanged(nameof(NameBox));
            }
        }
        public string NameBox
        {
            get => _nameBox;
            set
            {
                _nameBox = value;
                OnPropertyChanged(nameof(NameBox));
            }
        }
        #endregion

        #region Collections

        private readonly ObservableCollection<Ingredient> _allIngredients;
        public IEnumerable<Ingredient> AllIngredients => _allIngredients;

        #endregion

        #region Commands

        public ICommand? AddIngredientCommand { get; }
        public ICommand? UpdateIngredientCommand { get; }
        public ICommand? DeleteIngredientCommand { get; }
        
        #endregion

        public IngredientOverviewViewModel(IIngredientService ingredientService, IMedicineService medicineService)
        {
            _ingredientService = ingredientService;
            _medicineService = medicineService;

            _allIngredients = new ObservableCollection<Ingredient>();
            List<Ingredient> AllIng = _ingredientService.ReadAll().ToList();
            AllIng = AllIng.OrderBy(x => x.Name).ToList();
            foreach (var ingredient in AllIng)
            {
                _allIngredients.Add(ingredient);
            }

            AddIngredientCommand = new AddIngredientCommand(this);
            UpdateIngredientCommand = new UpdateIngredientCommand(this);
            DeleteIngredientCommand = new DeleteIngredientCommand(this);
        }
    }
}
