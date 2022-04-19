using System.Collections.Generic;

namespace HealthInstitution.Model
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

        private readonly IList<string> _illnessHistory;
        public IList<string> IllnessHistory { get => _illnessHistory; }

        private readonly IList<string> _allergens;
        public IList<string> Allergens { get => _allergens;}

        private Patient _patient;
        public virtual Patient Patient { get => _patient; set => OnPropertyChanged(ref _patient, value); }

        #endregion

        #region Constructor

        public MedicalRecord() { }
        public MedicalRecord(double height, double weight, Patient patient)
        {
            _height = height;
            _weight = weight;
            _illnessHistory = new List<string>();
            _allergens = new List<string>();
            _patient = patient;
        }

        #endregion

        #region Methods

        public void AddIllness(string illness)
        {
            foreach (string includedIllness in IllnessHistory)
            {
                if (includedIllness.Equals(illness))
                {
                    // Baci exception
                    return;
                }

                IllnessHistory.Add(illness);
            }
        }

        public void AddAllergen(string allergen)
        {
            foreach (string includedAllergen in Allergens)
            {
                if (includedAllergen.Equals(allergen))
                {
                    // Baci exception
                    return;
                }

                IllnessHistory.Add(allergen);
            }
        }

        #endregion
    }
}
