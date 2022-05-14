using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Model
{
    public class Referral : BaseObservableEntity
    {
        #region Attributes

        private Doctor? _doctor;
        public virtual Doctor Doctor { get => _doctor; set => OnPropertyChanged(ref _doctor, value); }

        private Patient _patient;
        public virtual Patient Patient { get => _patient; set => OnPropertyChanged(ref _patient, value); }

        private bool _isUsed;

        public bool IsUsed { get => _isUsed; set => OnPropertyChanged(ref _isUsed, value); }
        #endregion

        #region Constructor

        public Referral()
        {

        }

        #endregion
    }
}
