﻿using System;
using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Core.Features.UsersManagement.Model
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

        private bool _isRelevantForBan;
        public bool IsRelevantForBan { get => _isRelevantForBan; set => OnPropertyChanged(ref _isRelevantForBan, value); }

        #endregion

        #region Constructor

        public Activity()
        {

        }
        public Activity(Patient patient, DateTime dateOfAction, ActivityType activityType)
        {
            _patient = patient;
            _dateOfAction = dateOfAction;
            _activityType = activityType;
            _isRelevantForBan = true;
        }

        #endregion
    }
}
