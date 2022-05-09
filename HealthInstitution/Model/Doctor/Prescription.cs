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
        public IList<PrescribedMedicine> PrescribedMedicine { get => _prescribedMedicine;  set => OnPropertyChanged(ref _prescribedMedicine, value);}

        #endregion

        #region Constructor

        public Prescription()
        {

        }

        #endregion
    }
}
