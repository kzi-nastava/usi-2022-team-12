using System.Collections.Generic;

namespace HealthInstitution.Model
{
    public class Prescription : BaseObservableEntity
    {

        #region Attributes

        private IList<PrescribedMedicine> _prescribedMedicine;
        public IList<PrescribedMedicine> PrescribedMedicine { get => _prescribedMedicine; set => OnPropertyChanged(ref _prescribedMedicine, value); }

        #endregion

        #region Constructor

        public Prescription()
        {

        }

        #endregion
    }
}
