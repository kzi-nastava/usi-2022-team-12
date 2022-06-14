using System.Collections.Generic;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility.HelperClasses;
using HealthInstitution.Model.appointment;

namespace HealthInstitution.Core.Features.MedicalRecordManagement.Model
{
    /// <summary>
    /// Proveri dal ide virtual kod IList<string>
    /// </summary>
    public class MedicalRecord : BaseObservableEntity
    {
        #region Attributes

        private double _height;
        public double Height { get => _height; set => OnPropertyChanged(ref _height, value); }

        private double _weight;
        public double Weight { get => _weight; set => OnPropertyChanged(ref _weight, value); }

        private IList<Illness> _illnessHistory;
        public virtual IList<Illness> IllnessHistory { get => _illnessHistory; set => OnPropertyChanged(ref _illnessHistory, value); }

        private IList<Allergen> _allergens;
        public virtual IList<Allergen> Allergens { get => _allergens; set => OnPropertyChanged(ref _allergens, value); }

        private IList<PrescribedMedicine> _prescribedMedicines;
        public virtual IList<PrescribedMedicine> PrescribedMedicines { get => _prescribedMedicines; set => OnPropertyChanged(ref _prescribedMedicines, value); }

        private Patient _patient;
        public virtual Patient Patient { get => _patient; set => OnPropertyChanged(ref _patient, value); }

        #endregion

        #region Constructor

        public MedicalRecord() { }
        public MedicalRecord(double height, double weight, Patient patient)
        {
            _height = height;
            _weight = weight;
            _illnessHistory = new List<Illness>();
            _allergens = new List<Allergen>();
            _patient = patient;
        }

        public MedicalRecord(MedicalRecord medicalRecord) : base(medicalRecord)
        {
            _height = medicalRecord._height;
            _weight = medicalRecord._weight;
            _patient = medicalRecord._patient;
            _illnessHistory = new List<Illness>();
            _allergens = new List<Allergen>();
            foreach (var illness in medicalRecord.IllnessHistory)
            {
                _illnessHistory.Add(illness);
            }
            foreach (var allergen in medicalRecord.Allergens)
            {
                _allergens.Add(allergen);
            }
        }
        #endregion

        #region Methods

        public void AddIllness(Illness illness)
        {
            foreach (Illness includedIllness in IllnessHistory)
            {
                if (includedIllness.Equals(illness))
                {
                    // Baci exception
                    return;
                }
            }
            IllnessHistory.Add(illness);
        }

        public void AddAllergen(Allergen allergen)
        {
            foreach (Allergen includedAllergen in Allergens)
            {
                if (includedAllergen.Equals(allergen))
                {
                    // Baci exception
                    return;
                }
            }
            Allergens.Add(allergen);
        }

        #endregion
    }
}
