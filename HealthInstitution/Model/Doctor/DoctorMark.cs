using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Model
{
    public class DoctorMark : BaseObservableEntity
    {
        #region Attributes

        private Doctor _doctor;
        public virtual Doctor Doctor { get => _doctor; set => OnPropertyChanged(ref _doctor, value); }

        private int _mark;
        public int Mark { get => _mark; set => OnPropertyChanged(ref _mark, value); }

        #endregion

        #region Constructor

        public DoctorMark()
        {

        }

        #endregion

    }
}
