using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class PrescriptionViewModel : ViewModelBase
    {
        #region Atributes
        private IMedicineService _medicineService;
        private IMedicalRecordService _medicalRecordService;
        private string _searchText;
        private string _instruction;
        private string _usage;
        private Medicine _selectedMedicine;
        private MedicalRecord _medicalRecord;
        #endregion

        #region Properties
        public IMedicineService MedicineService => _medicineService;
        public IMedicalRecordService MedicalRecordService => _medicalRecordService;
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

        public string Usage
        {
            get => _usage;
            set
            {
                _usage = value;
                OnPropertyChanged(nameof(Usage));
            }
        }
        #endregion

        #region Collections
        private ObservableCollection<PrescribedMedicine> _prescribedMedicines;
        public IEnumerable<PrescribedMedicine> PrescribedMedicines => _prescribedMedicines;

        private ObservableCollection<Medicine> _medicines;
        public IEnumerable<Medicine> Medicines => _medicines;

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
            IEnumerable<Medicine> medicines = _medicineService.GetApprovedMedicine();
            foreach(Medicine medicine in medicines)
            {
                _medicines.Add(medicine);
            }
            _prescribedMedicines = new ObservableCollection<PrescribedMedicine>();
            List<PrescribedMedicine> prescribedMedicines = GlobalStore.ReadObject<List<PrescribedMedicine>>("Prescription");
            foreach(PrescribedMedicine prescribedMedicine in prescribedMedicines)
            {
                _prescribedMedicines.Add(prescribedMedicine);
            }

            BackToExaminationCommand = new BackToExaminationCommandFromPrescription(this);
            SearchMedicineCommand = new SearchMedicineCommand(this);
            PrescribeMedicineCommand = new PrescribeMedicineCommand(this);
        }

        public void UpdateData(string prefix)
        {
            _medicines = new ObservableCollection<Medicine>();
            IEnumerable<Medicine> medicines;
            if (string.IsNullOrEmpty(prefix))
            {
                medicines = _medicineService.GetApprovedMedicine();
            }
            else
            {
                medicines = _medicineService.FilterMedicineBySearchText(prefix);
            }
            foreach (Medicine medicine in medicines)
            {
                _medicines.Add(medicine);
            }
            OnPropertyChanged(nameof(Medicines));
        }
        public void addPrescribedMedicine(PrescribedMedicine prescribedMedicine)
        {
            _prescribedMedicines.Add(prescribedMedicine);
            OnPropertyChanged(nameof(PrescribedMedicines));
        }
    }
}
