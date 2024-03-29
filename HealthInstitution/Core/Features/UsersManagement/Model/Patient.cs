﻿using System.Collections.Generic;

namespace HealthInstitution.Core.Features.UsersManagement.Model
{
    public class Patient : User
    {
        #region Attributes

        private bool _isBlocked;
        public bool IsBlocked { get => _isBlocked; set => OnPropertyChanged(ref _isBlocked, value); }

        public BlockType _blockType;
        public BlockType BlockType { get => _blockType; set => OnPropertyChanged(ref _blockType, value); }

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
