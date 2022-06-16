using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Core.Features.MedicalRecordManagement.Model
{
    public class Illness : BaseObservableEntity
    {
        #region Attributes

        private string _name;

        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        #endregion

        #region Constructor
        public Illness() { }
        public Illness(string name)
        {
            Name = name;
        }
        #endregion
    }
}
