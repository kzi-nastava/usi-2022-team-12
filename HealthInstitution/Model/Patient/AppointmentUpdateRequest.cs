using System;

namespace HealthInstitution.Model
{
    public class AppointmentUpdateRequest : AppointmentRequest
    {
        private DateTime _startDate;
        public DateTime StartDate { get => _startDate; set => OnPropertyChanged(ref _startDate, value); }

        private DateTime _endDate;
        public DateTime EndDate { get => _endDate; set => OnPropertyChanged(ref _endDate, value); }

        private Doctor _doctor;
        public virtual Doctor Doctor { get => _doctor; set => OnPropertyChanged(ref _doctor, value); }

        private Room _room;
        public virtual Room Room { get => _room; set => OnPropertyChanged(ref _room, value); }

        public AppointmentUpdateRequest() { }
    }
}
