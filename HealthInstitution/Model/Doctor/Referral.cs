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
        public Doctor Doctor { get => _doctor; set => OnPropertyChanged(ref _doctor, value); }

        private DoctorSpecialization? _specialization;
        public DoctorSpecialization? DoctorSpecialization { get => _specialization; set => OnPropertyChanged(ref _specialization, value); }

        private Patient _patient;
        public Patient Patient { get => _patient; set => OnPropertyChanged(ref _patient, value); }

        #endregion

        #region Constructor

        public Referral()
        {

        }

        #endregion
    }
}
