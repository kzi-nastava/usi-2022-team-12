using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Implementation;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class ExaminationViewModel : ViewModelBase
    {
        #region Atributes
        private readonly IMedicalRecordService _medicalRecordService;

        private readonly IIllnessService _illnessService;

        private readonly IAllergenService _allergenService;

        private readonly IAppointmentService _appointmentService;

        private readonly Patient _patient;

        private readonly Appointment _appointment;

        private readonly MedicalRecord _medicalRecord;

        private MedicalRecord _updatedMedicalRecord;

        private string _anamnesis;

        private string _newIllnessName;

        private string _newAllergenName;

        #endregion Atributes

        #region Properties
        public IIllnessService IllnessService => _illnessService;
        public IAllergenService AllergenService => _allergenService;
        public IMedicalRecordService MedicalRecordService => _medicalRecordService;
        public IAppointmentService AppointmentService => _appointmentService;
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
                OnPropertyChanged(Anamnesis);
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
                _updatedMedicalRecord.Weight =  value;
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
                foreach(var illness in value)
                {
                    _illnessHistoryData.Add(illness);
                }
                OnPropertyChanged(nameof(Illness));
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
                foreach(var allergen in value)
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
        #endregion Commands
        public ExaminationViewModel(IMedicalRecordService medicalRecordService, IIllnessService illnessService, IAllergenService allergenService, IAppointmentService appointmentService, Appointment appointment)
        {
            _anamnesis = "";
            _newIllnessName = "";
            _newAllergenName = "";
            _newIllnessName = "";
            _newAllergens = new List<Allergen>();
            _newIllnesses = new List<Illness>();
            _medicalRecordService = medicalRecordService;
            _illnessService = illnessService;
            _allergenService = allergenService;
            _appointmentService = appointmentService;
            _appointment = appointment;
            _patient = _appointment.Patient;
            _medicalRecord = medicalRecordService.GetMedicalRecordForPatient(_patient);
            _updatedMedicalRecord = new MedicalRecord(_medicalRecord);
            IllnessHistoryData = _updatedMedicalRecord.IllnessHistory;
            Allergens = _updatedMedicalRecord.Allergens;
            CancelExaminationCommand = new NavigateScheduleCommand();
            UndoChangesCommand = new UndoMRChangesCommand(this);
            AddIllnessCommand = new AddIllnessCommand(this);
            AddAllergenCommand = new AddAllergenCommand(this);
            FinishExaminationCommand = new FinishExaminationCommand(this);
            PropertyChanged += OnUndo;
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
