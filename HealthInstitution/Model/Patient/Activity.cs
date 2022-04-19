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

        #endregion

        #region Constructor

        public Activity(DateTime dateOfAction, ActivityType activityType)
        {
            _dateOfAction = dateOfAction;
            _activityType = activityType;
        }

        #endregion
    }
}
