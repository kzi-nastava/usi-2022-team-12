using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthInstitution.Commands.doctor;
using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;
using HealthInstitution.Model;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.ViewModel.doctor
{
    public class PrescriptionViewModel : ViewModelBase, ISearchMedicineViewModel
    {
        #region Atributes
        private IMedicineService _medicineService;
        private string _searchText;
        private string _instruction;
        private int _usageHourSpan;
        private DateTime _usageStart;
        private DateTime _usageEnd;
        private Medicine _selectedMedicine;
        private MedicalRecord _medicalRecord;
        #endregion

        #region Properties
        public IMedicineService MedicineService => _medicineService;
        public MedicalRecord MedicalRecord => _medicalRecord;
        public Medicine SelectedMedicine
        {
            get => _selectedMedicine;
            set
            {
                _selectedMedicine = value;
                OnPropertyChanged(nameof(SelectedMedicine));
            }
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }
        public string Instruction
        {
            get => _instruction;
            set
            {
                _instruction = value;
                OnPropertyChanged(nameof(Instruction));
            }
        }

        public int UsageHourSpan
        {
            get => _usageHourSpan;
            set
            {
                _usageHourSpan = value;
                OnPropertyChanged(nameof(UsageHourSpan));
            }
        }

        public DateTime UsageStart
        {
            get => _usageStart;
            set
            {
                _usageStart = value;
                OnPropertyChanged(nameof(UsageStart));
            }
        }

        public DateTime UsageEnd
        {
            get => _usageEnd;
            set
            {
                _usageEnd = value;
                OnPropertyChanged(nameof(UsageEnd));
            }
        }
        #endregion

        #region Collections
        private ObservableCollection<PrescribedMedicine> _prescribedMedicines;
        public IEnumerable<PrescribedMedicine> PrescribedMedicines => _prescribedMedicines;

        private ObservableCollection<Medicine> _medicines;
        public IEnumerable<Medicine> Medicines
        {
            get => _medicines;
            set
            {
                _medicines = new ObservableCollection<Medicine>();
                foreach (Medicine medicine in value)
                {
                    _medicines.Add(medicine);
                }
                OnPropertyChanged(nameof(Medicines));
            }
        }

        #endregion

        #region Commands
        public ICommand PrescribeMedicineCommand { get; }
        public ICommand BackToExaminationCommand { get; }
        public ICommand SearchMedicineCommand { get; }
        #endregion
        public PrescriptionViewModel(IMedicineService medicineService, MedicalRecord medicalRecord)
        {
            _medicalRecord = medicalRecord;
            _medicineService = medicineService;
            _medicines = new ObservableCollection<Medicine>();
            UsageStart = DateTime.Now;
            UsageEnd = DateTime.Now;
            IEnumerable<Medicine> medicines = _medicineService.GetApprovedMedicine();
            foreach (Medicine medicine in medicines)
            {
                _medicines.Add(medicine);
            }
            _prescribedMedicines = new ObservableCollection<PrescribedMedicine>();
            List<PrescribedMedicine> prescribedMedicines = GlobalStore.ReadObject<List<PrescribedMedicine>>("Prescription");
            foreach (PrescribedMedicine prescribedMedicine in prescribedMedicines)
            {
                _prescribedMedicines.Add(prescribedMedicine);
            }

            BackToExaminationCommand = new BackToExaminationCommandFromPrescription(this);
            SearchMedicineCommand = new SearchMedicineCommand(this, _medicineService, Status.Approved);
            PrescribeMedicineCommand = new PrescribeMedicineCommand(this);
        }
        public void addPrescribedMedicine(PrescribedMedicine prescribedMedicine)
        {
            _prescribedMedicines.Add(prescribedMedicine);
            OnPropertyChanged(nameof(PrescribedMedicines));
        }
    }
}
