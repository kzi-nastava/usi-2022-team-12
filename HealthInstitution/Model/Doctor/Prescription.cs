using System.Collections.Generic;

namespace HealthInstitution.Model
{
    public class Prescription : BaseObservableEntity
    {

        #region Attributes

        private IList<PrescribedMedicine> _prescribedMedicines;
        public virtual IList<PrescribedMedicine> PrescribedMedicines { get => _prescribedMedicines;  set => OnPropertyChanged(ref _prescribedMedicines, value);}

        #endregion

        #region Constructor

        public Prescription()
        {
            _prescribedMedicines = new List<PrescribedMedicine>();
        }

        #endregion

    }
}
