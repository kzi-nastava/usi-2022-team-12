using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Model.appointment
{
    public class Allergen : BaseObservableEntity
    {
        #region Attributes

        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        #endregion

        #region Constructor

        public Allergen() { }
        public Allergen(string name)
        {
            Name = name;
        }

        #endregion

    }
}
