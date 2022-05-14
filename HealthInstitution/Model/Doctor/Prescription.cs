using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Model
{
    public class Prescription : BaseObservableEntity
    {

        #region Attributes

        private IList<PrescribedMedicine> _prescribedMedicine;
        public virtual IList<PrescribedMedicine> PrescribedMedicine { get => _prescribedMedicine;  set => OnPropertyChanged(ref _prescribedMedicine, value);}

        #endregion

        #region Constructor

        public Prescription()
        {
            _prescribedMedicine = new List<PrescribedMedicine>();
        }

        #endregion

    }
}
