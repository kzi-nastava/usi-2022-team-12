using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using HealthInstitution.Core.Features.AppointmentScheduling.Commands.DoctorCMD;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.EquipmentManagement.Repository;
using HealthInstitution.Core.Features.MedicalRecordManagement.Commands.DoctorCMD;
using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Features.MedicalRecordManagement.Repository;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Features.MedicineManagement.Repository;
using HealthInstitution.Core.Features.OperationsAndExaminations.Commands.DoctorCMD;
using HealthInstitution.Core.Features.OperationsAndExaminations.Model;
using HealthInstitution.Core.Features.OperationsAndExaminations.Repository;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.GUI.Utility.Dialog.Service;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.OperationsAndExaminations
{
    public class ExaminationViewModel : ViewModelBase
    {
        #region Atributes
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        private readonly IEntryRepository _entryRepository;

        private readonly IIllnessRepository _illnessRepository;

        private readonly IAllergenRepository _allergenRepository;

        private readonly ISchedulingService _schedulingService;

        private readonly IReferralRepository _referralRepository;

        private readonly IPrescribedMedicineRepository _prescribedMedicineRepository;

        private readonly IDialogService _dialogService;

        private readonly Patient _patient;

        private readonly Appointment _appointment;

        private readonly MedicalRecord _medicalRecord;

        private MedicalRecord _updatedMedicalRecord;

        private string _anamnesis;

        private string _newIllnessName;

        private string _newAllergenName;

        #endregion Atributes

        #region Properties
        public IEntryRepository EntryRepository => _entryRepository;
        public IIllnessRepository IllnessRepository => _illnessRepository;
        public IAllergenRepository AllergenRepository => _allergenRepository;
        public IMedicalRecordRepository MedicalRecordRepository => _medicalRecordRepository;
        public ISchedulingService SchedulingService => _schedulingService;
        public IReferralRepository ReferralRepository => _referralRepository;
        public IPrescribedMedicineRepository PrescribedMedicineRepository => _prescribedMedicineRepository;
        public IDialogService DialogService => _dialogService;
        public MedicalRecord MedicalRecord => _medicalRecord;
        public Appointment Appointment => _appointment;
        public string NewAllergenName
        {
            get => _newAllergenName;
            set
            {
                _newAllergenName = value;
                OnPropertyChanged(nameof(NewAllergenName));
            }
        }
        public string NewIllnessName
        {
            get => _newIllnessName;
            set
            {
                _newIllnessName = value;
                OnPropertyChanged(nameof(NewIllnessName));
            }
        }
        public string Anamnesis
        {
            get => _anamnesis;
            set
            {
                _anamnesis = value;
                OnPropertyChanged(nameof(Anamnesis));
            }
        }
        public MedicalRecord UpdatedMedicalRecord
        {
            get => _updatedMedicalRecord;
            set
            {
                _updatedMedicalRecord = value;
                OnPropertyChanged(nameof(UpdatedMedicalRecord));
            }
        }
        public string PatientFullName => _patient.FullName;
        public double Height
        {
            get => _updatedMedicalRecord.Height;
            set
            {
                _updatedMedicalRecord.Height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        public string Age => CalculateAge(_patient.DateOfBirth).ToString();

        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Subtract(dateOfBirth).Days;
            age = age / 365;
            return age;
        }
        public double Weight
        {
            get => _updatedMedicalRecord.Weight;
            set
            {
                _updatedMedicalRecord.Weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }
        #endregion Properties

        #region Collections
        private List<Illness> _newIllnesses;

        private List<Allergen> _newAllergens;
        public List<Illness> NewIllnesses => _newIllnesses;
        public List<Allergen> NewAllergens => _newAllergens;

        private ObservableCollection<Illness> _illnessHistoryData;
        public IEnumerable<Illness> IllnessHistoryData
        {
            get => _illnessHistoryData;
            set
            {
                _illnessHistoryData = new ObservableCollection<Illness>();
                foreach (var illness in value)
                {
                    _illnessHistoryData.Add(illness);
                }
                OnPropertyChanged(nameof(IllnessHistoryData));
            }
        }
        public void AddIllness(Illness illness)
        {
            _illnessHistoryData.Add(illness);
            _newIllnesses.Add(illness);
            OnPropertyChanged(nameof(IllnessHistoryData));
        }

        private ObservableCollection<Allergen> _allergens;
        public IEnumerable<Allergen> Allergens
        {
            get => _allergens;
            set
            {
                _allergens = new ObservableCollection<Allergen>();
                foreach (var allergen in value)
                {
                    _allergens.Add(allergen);
                }
                OnPropertyChanged(nameof(Allergens));
            }
        }
        public void AddAllergens(Allergen allergen)
        {
            _allergens.Add(allergen);
            _newAllergens.Add(allergen);
            OnPropertyChanged(nameof(Allergens));
        }
        #endregion Collections

        #region Commands
        public ICommand CancelExaminationCommand { get; }
        public ICommand UndoChangesCommand { get; }
        public ICommand AddIllnessCommand { get; }
        public ICommand AddAllergenCommand { get; }
        public ICommand FinishExaminationCommand { get; }
        public ICommand CreateReferralCommand { get; }
        public ICommand PrescriptionCommand { get; }
        #endregion Commands
        public ExaminationViewModel(IMedicalRecordRepository medicalRecordRepository, IIllnessRepository illnessRepository, IAllergenRepository allergenRepository, ISchedulingService schedulingService, IReferralRepository referralRepository, IPrescribedMedicineRepository prescribedMedicineRepository, IDialogService dialogService, IEntryRepository entryRepository, Appointment appointment)
        {
            _anamnesis = "";
            _newIllnessName = "";
            _newAllergenName = "";
            _newIllnessName = "";
            _newAllergens = new List<Allergen>();
            _newIllnesses = new List<Illness>();
            _medicalRecordRepository = medicalRecordRepository;
            _entryRepository = entryRepository;
            _dialogService = dialogService;
            _illnessRepository = illnessRepository;
            _referralRepository = referralRepository;
            _allergenRepository = allergenRepository;
            _schedulingService = schedulingService;
            _prescribedMedicineRepository = prescribedMedicineRepository;
            _appointment = appointment;
            _patient = _appointment.Patient;
            _medicalRecord = _medicalRecordRepository.GetMedicalRecordForPatient(_patient);
            _updatedMedicalRecord = new MedicalRecord(_medicalRecord);
            IllnessHistoryData = _updatedMedicalRecord.IllnessHistory;
            Allergens = _updatedMedicalRecord.Allergens;
            CancelExaminationCommand = new NavigateScheduleCommand();
            UndoChangesCommand = new UndoMRChangesCommand(this);
            AddIllnessCommand = new AddIllnessCommand(this);
            AddAllergenCommand = new AddAllergenCommand(this);
            FinishExaminationCommand = new FinishExaminationCommand(this);
            CreateReferralCommand = new NavigateReferralCommand();
            PrescriptionCommand = new NavigatePrescriptionCommand();
            PropertyChanged += OnUndo;
            GlobalStore.AddObject("NewReferrals", new List<Referral>());
            GlobalStore.AddObject("Prescription", new List<PrescribedMedicine>());
        }
        private void OnUndo(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UpdatedMedicalRecord))
            {
                OnPropertyChanged(nameof(Height));
                OnPropertyChanged(nameof(Weight));
                OnPropertyChanged(nameof(Allergens));
                OnPropertyChanged(nameof(IllnessHistoryData));
            }
        }
    }
}
