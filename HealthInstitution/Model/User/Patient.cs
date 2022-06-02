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

        private int notificationPreference;
        public int NotificationPreference { get => notificationPreference; set => OnPropertyChanged(ref notificationPreference, value); }

        #endregion

        #region Constructor

        public Patient()
        {
            _isBlocked = false;
        }

        #endregion
    }
}
