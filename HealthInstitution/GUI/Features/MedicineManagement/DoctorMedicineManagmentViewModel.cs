﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthInstitution.Core.Features.MedicineManagement.Commands.DoctorCMD;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Features.MedicineManagement.Repository;
using HealthInstitution.Core.Features.MedicineManagement.Service;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility.HelperClasses;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.MedicineManagement
{

    public class DoctorMedicineManagmentViewModel : ViewModelBase, ISearchMedicineViewModel
    {
        #region Atributes
        private readonly IMedicineService _medicineService;
        private readonly IMedicineReviewRepository _medicineReviewRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly Doctor _doctor;
        private string _searchText;
        private Medicine? _selectedMedicine;
        private string _revisionComment;
        #endregion

        #region Properties
        public Doctor Doctor => _doctor;
        public IMedicineService MedicineService => _medicineService;
        public IMedicineReviewRepository MedicineReviewRepository => _medicineReviewRepository;
        public IMedicineRepository MedicineRepository => _medicineRepository;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }
        public Medicine? SelectedMedicine
        {
            get => _selectedMedicine;
            set
            {
                _selectedMedicine = value;
                if (value != null)
                {
                    Ingredients = value.Ingredients;
                }
                OnPropertyChanged(nameof(SelectedMedicine));
            }
        }
        public string RevisionComment
        {
            get => _revisionComment;
            set
            {
                _revisionComment = value;
                OnPropertyChanged(nameof(RevisionComment));
            }
        }
        #endregion

        #region Collections
        private ObservableCollection<Medicine> _medicines;
        public IEnumerable<Medicine> Medicines
        {
            get => _medicines;
            set
            {
                _medicines = new ObservableCollection<Medicine>();
                foreach (var medicine in value)
                {
                    _medicines.Add(medicine);
                }
                OnPropertyChanged(nameof(Medicines));
            }
        }

        private ObservableCollection<Ingredient>? _ingredients;
        public IEnumerable<Ingredient>? Ingredients
        {
            get => _ingredients;
            set
            {
                if (value == null)
                {
                    _ingredients = null;
                }
                else
                {
                    _ingredients = new ObservableCollection<Ingredient>();
                    foreach (var ingredient in value)
                    {
                        _ingredients.Add(ingredient);
                    }
                }
                OnPropertyChanged(nameof(Ingredients));
            }
        }
        #endregion

        #region Commands
        public ICommand SearchMedicineCommand { get; }
        public ICommand ApproveMedicineCommand { get; }
        public ICommand RejectMedicineCommand { get; }
        #endregion
        public DoctorMedicineManagmentViewModel(IMedicineService medicineService, IMedicineRepository medicineRepository, IMedicineReviewRepository medicineReviewRepository)
        {
            _medicineService = medicineService;
            _medicineReviewRepository = medicineReviewRepository;
            _medicineRepository = medicineRepository;
            _doctor = (Doctor)GlobalStore.ReadObject<Doctor>("LoggedUser");
            _searchText = "";
            _revisionComment = "";
            IEnumerable<Medicine> medicines = _medicineService.GetPendingMedicine();
            Medicines = medicines;
            SearchMedicineCommand = new SearchMedicineCommand(this, _medicineService, Status.Pending);
            ApproveMedicineCommand = new ApproveMedicineCommand(this);
            RejectMedicineCommand = new MarkForRevisionCommand(this);
        }
    }
}
