using HealthInstitution.Core.Utility.HelperClasses;
using System;

namespace HealthInstitution.Core.Features.NotificationManagement.Model
{
    public class UserNotification : BaseObservableEntity
    {
        #region Attributes

        private Guid _userId;
        public Guid UserId { get => _userId; set => OnPropertyChanged(ref _userId, value); }

        private string _content;
        public string Content { get => _content; set => OnPropertyChanged(ref _content, value); }

        private bool _isShown;
        public bool IsShown { get => _isShown; set => OnPropertyChanged(ref _isShown, value); }

        #endregion

        #region Constructor

        public UserNotification()
        {

        }

        #endregion
    }
}
