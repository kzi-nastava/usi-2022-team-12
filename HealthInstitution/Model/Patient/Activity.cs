using System;

namespace HealthInstitution.Model
{
    public class Activity : BaseObservableEntity
    {

        #region Attributes

        private DateTime _dateOfAction;
        public DateTime DateOfAction { get => _dateOfAction; set => OnPropertyChanged(ref _dateOfAction, value); }

        private ActivityType _activityType;
        public ActivityType ActivityType { get => _activityType; set => OnPropertyChanged(ref _activityType, value); }

        private Patient _patient;
        public virtual Patient Patient { get => _patient; set => OnPropertyChanged(ref _patient, value); }
        #endregion

        #region Constructor

        public Activity(Patient pt, DateTime dateOfAction, ActivityType activityType)
        {
            _patient = pt;
            _dateOfAction = dateOfAction;
            _activityType = activityType;
        }

        #endregion
    }
}
