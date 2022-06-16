using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Core.Features.MedicineManagement.Commands.ManagerCMD;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Features.MedicineManagement.Repository;
using HealthInstitution.Core.Features.MedicineManagement.Service;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.MedicineManagement
{
    public class MedicineOverviewViewModel : ViewModelBase
    {
        #region Attributes

        private IMedicineService _medicineService;
        private IIngredientRepository _ingredientRepository;
        private IMedicineReviewRepository _medicineReviewRepository;
        private IMedicineRepository _medicineRepository;
        private string _nameBox;
        private string _descriptionBox;
        private string _revisionMedicineName;
        private string _commentBox;
        private bool _isRevision;
        private Ingredient _selectedIngredient;
        private Ingredient _selectedIngredientNew;
        private MedicineReview _selectedRejectedMedicine;


        #endregion

        #region Properties

        public IMedicineService MedicineService
        {
            get => _medicineService;
        }
        public IIngredientRepository IngredientRepository
        {
            get => _ingredientRepository;
        }
        public IMedicineReviewRepository MedicineReviewRepository
        {
            get => _medicineReviewRepository;
        }
        public IMedicineRepository MedicineRepository
        {
            get => _medicineRepository;
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
        public string DescriptionBox
        {
            get => _descriptionBox;
            set
            {
                _descriptionBox = value;
                OnPropertyChanged(nameof(DescriptionBox));
            }
        }
        public string RevisionMedicineName
        {
            get => _revisionMedicineName;
            set
            {
                _revisionMedicineName = value;
                OnPropertyChanged(nameof(RevisionMedicineName));
            }
        }
        public string CommentBox
        {
            get => _commentBox;
            set
            {
                _commentBox = value;
                OnPropertyChanged(nameof(CommentBox));
            }
        }
        public Ingredient SelectedIngredient
        {
            get => _selectedIngredient;
            set
            {
                _selectedIngredient = value;
                OnPropertyChanged(nameof(SelectedIngredient));
                AddIngredientForMedicine(value);
                OnPropertyChanged(nameof(NewIngredients));
                OnPropertyChanged(nameof(AllIngredients));
            }
        }
        public Ingredient SelectedIngredientNew
        {
            get => _selectedIngredientNew;
            set
            {
                _selectedIngredientNew = value;
                OnPropertyChanged(nameof(SelectedIngredientNew));
                RemoveIngredientForMedicine(value);
                OnPropertyChanged(nameof(NewIngredients));
                OnPropertyChanged(nameof(AllIngredients));
            }
        }


        public bool IsRevision
        {
            get { return _isRevision; }
            set
            {
                if (_isRevision == value) return;

                _isRevision = value;
                OnPropertyChanged(nameof(IsRevision));
            }
        }

        public MedicineReview SelectedRejectedMedicine
        {
            get => _selectedRejectedMedicine;
            set
            {
                _selectedRejectedMedicine = value;
                GlobalStore.AddObject("SelectedRejectedMedicine", value);
                OnPropertyChanged(nameof(SelectedRejectedMedicine));
                _isRevision = true;
                _nameBox = _selectedRejectedMedicine.Medicine.Name;
                _descriptionBox = _selectedRejectedMedicine.Medicine.Description;
                _newIngredients.Clear();
                foreach (var ingredient in _selectedRejectedMedicine.Medicine.Ingredients)
                {
                    _newIngredients.Add(ingredient);
                }
                _revisionMedicineName = _selectedRejectedMedicine.Medicine.Name;
                _commentBox = _selectedRejectedMedicine.Comment;
                OnPropertyChanged(nameof(IsRevision));
                OnPropertyChanged(nameof(NameBox));
                OnPropertyChanged(nameof(DescriptionBox));
                OnPropertyChanged(nameof(NewIngredients));
                OnPropertyChanged(nameof(RevisionMedicineName));
                OnPropertyChanged(nameof(CommentBox));
            }
        }

        #endregion

        #region Collections

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

        private readonly ObservableCollection<Ingredient> _allIngredients;
        private readonly ObservableCollection<Ingredient> _newIngredients;
        private readonly ObservableCollection<MedicineReview> _rejectedMedicine;
        public IEnumerable<Ingredient> AllIngredients => _allIngredients;
        public IEnumerable<Ingredient> NewIngredients => _newIngredients;
        public IEnumerable<MedicineReview> RejectedMedicine => _rejectedMedicine;

        void AddIngredientForMedicine(Ingredient value)
        {
            if (NewIngredients.Count() != 0)
            {
                foreach (Ingredient ing in NewIngredients)
                {
                    if (ing.Name == value.Name) { return; }
                }
                this._newIngredients.Add(value);
                return;

            }
            this._newIngredients.Add(value);

        }
        void RemoveIngredientForMedicine(Ingredient value)
        {
            _newIngredients.Remove(value);
        }

        #endregion

        #region Commands

        public ICommand? SendMedicineRequestCommand { get; }
        public ICommand? RemoveRejectedCommand { get; }

        #endregion

        public MedicineOverviewViewModel(IMedicineService medicineService, IIngredientRepository ingredientRepository, IMedicineReviewRepository medicineReviewRepository, IMedicineRepository medicineRepository)
        {
            _medicineService = medicineService;
            _ingredientRepository = ingredientRepository;
            _medicineReviewRepository = medicineReviewRepository;
            _medicineRepository = medicineRepository;
            AllMedicine = _medicineService.GetApprovedMedicine().ToList();
            AllMedicine = AllMedicine.OrderBy(x => x.Name).ToList();

            SendMedicineRequestCommand = new SendMedicineRequestCommand(this);
            RemoveRejectedCommand = new RemoveRejectedCommand(this);

            _allIngredients = new ObservableCollection<Ingredient>();
            _newIngredients = new ObservableCollection<Ingredient>();
            _rejectedMedicine = new ObservableCollection<MedicineReview>();

            List<Ingredient> AllIng = _ingredientRepository.ReadAll().ToList();
            AllIng = AllIng.OrderBy(x => x.Name).ToList();
            foreach (var ingredient in AllIng)
            {
                _allIngredients.Add(ingredient);
            }

            List<MedicineReview> MedRew = _medicineReviewRepository.ReadAll().ToList();
            MedRew = MedRew.OrderBy(x => x.Medicine.Name).ToList();
            foreach (var med in MedRew)
            {
                _rejectedMedicine.Add(med);
            }

        }
    }
}
