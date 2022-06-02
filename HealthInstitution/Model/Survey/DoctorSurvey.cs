using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Model
{
    public class DoctorSurvey : BaseObservableEntity
    {
        #region Attributes

        private Doctor _doctor;
        public virtual Doctor Doctor { get => _doctor; set => OnPropertyChanged(ref _doctor, value); }

        private int _serviceQuality;
        public int ServiceQuality { get => _serviceQuality; set => OnPropertyChanged(ref _serviceQuality, value); }

        private int _recommendation;
        public int Recommendation { get => _recommendation; set => OnPropertyChanged(ref _recommendation, value); }

        private string? _comment;
        public string? Comment { get => _comment; set => OnPropertyChanged(ref _comment, value); }

        #endregion

        #region Constructor

        public DoctorSurvey()
        {

        }

        #endregion

    }
}
