using System.Collections.Generic;

namespace HealthInstitution.Model
{
    public class Patient : User
    {
        #region Attributes

        private bool _isBlocked;
        public bool IsBlocked { get => _isBlocked; set => OnPropertyChanged(ref _isBlocked, value); }

        private IList<Activity> _activities;
        public virtual IList<Activity> Activities { get => _activities; set => OnPropertyChanged(ref _activities, value); }

        #endregion

        #region Constructor

        public Patient()
        {
            _isBlocked = false;
            _activities = new List<Activity>();
        }

        #endregion

        #region Methods

        public void AddActivity(Activity activity)
        {
            foreach (var includedActivity in _activities) {
                if (includedActivity.Id == activity.Id)
                {
                    return;
                }
            }
            _activities.Add(activity);
        }

        #endregion
    }
}
