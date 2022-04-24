using System.Collections.Generic;

namespace HealthInstitution.Model
{
    public class Patient : User
    {
        #region Attributes

        private bool _isBlocked;
        public bool IsBlocked { get => _isBlocked; set => OnPropertyChanged(ref _isBlocked, value); }

        #endregion

        #region Constructor

        public Patient()
        {
            _isBlocked = false;
            _activities = new List<Activity>();
        }

        #endregion
    }
}
