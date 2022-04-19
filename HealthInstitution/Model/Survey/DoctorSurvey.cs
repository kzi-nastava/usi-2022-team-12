using System;
using System.Collections.Generic;

namespace HealthInstitution.Model.Survey
{
    public class DoctorSurvey : BaseObservableEntity
    {
        #region Attributes

        private Guid _doctorId;
        public Guid DoctorId { get => _doctorId; set => OnPropertyChanged(ref _doctorId, value); }

        private readonly List<SurveyItem<DoctorSurvey>> _surveyItems;
        public virtual List<SurveyItem<DoctorSurvey>> SurveyItems { get => _surveyItems; }

        #endregion

        #region Constructor 

        public DoctorSurvey(Guid doctorId)
        {
            _doctorId = doctorId;
            _surveyItems = new List<SurveyItem<DoctorSurvey>>();
        }

        #endregion

        #region Methods

        public void CreateSurveyItem(string itemDescription)
        {
            var newItem = new SurveyItem<DoctorSurvey>(itemDescription, this);
            _surveyItems.Add(newItem);
        }

        #endregion
    }
}
