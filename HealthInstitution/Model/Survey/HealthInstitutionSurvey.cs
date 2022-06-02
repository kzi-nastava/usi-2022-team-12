using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Model
{
    public class HealthInstitutionSurvey : BaseObservableEntity
    {
        #region Attributes

        private Patient _patient;
        public virtual Patient Patient { get => _patient; set => OnPropertyChanged(ref _patient, value); }

        private int _cleanliness;
        public int Cleanliness { get => _cleanliness; set => OnPropertyChanged(ref _cleanliness, value); }

        private int _serviceQuality;
        public int ServiceQuality { get => _serviceQuality; set => OnPropertyChanged(ref _serviceQuality, value); }

        private int _serviceSatisfaction;
        public int ServiceSatisfaction { get => _serviceSatisfaction; set => OnPropertyChanged(ref _serviceSatisfaction, value); }

        private int _recommendation;
        public int Recommendation { get => _recommendation; set => OnPropertyChanged(ref _recommendation, value); }

        private string? _comment;
        public string? Comment { get => _comment; set => OnPropertyChanged(ref _comment, value); }

        #endregion

        #region Constructor

        public HealthInstitutionSurvey()
        {

        }

        #endregion
    }
}
