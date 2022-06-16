using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility.HelperClasses;
using System;

namespace HealthInstitution.Core.Features.OffDaysManagement.Model
{
    public class OffDaysRequest : BaseObservableEntity
    {
        #region Atributes

        private DateTime _startDate;
        public DateTime StartDate { get => _startDate; set => OnPropertyChanged(ref _startDate, value); }

        private DateTime _endDate;
        public DateTime EndDate { get => _endDate; set => OnPropertyChanged(ref _endDate, value); }

        private string _reason;
        public string Reason { get => _reason; set => OnPropertyChanged(ref _reason, value); }

        private Doctor _doctor;
        public virtual Doctor Doctor { get => _doctor; set => OnPropertyChanged(ref _doctor, value); }

        private bool _isUrgent;
        public bool IsUrgent { get => _isUrgent; set => OnPropertyChanged(ref _isUrgent, value); }

        private Status _status;
        public Status Status { get => _status; set => OnPropertyChanged(ref _status, value); }

        private string? _refuseComment;
        public string? RefuseComment { get => _refuseComment; set => OnPropertyChanged(ref _refuseComment, value); }

        #endregion

        #region Constructor

        public OffDaysRequest() { }

        #endregion
    }
}
