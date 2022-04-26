using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public AppointmentUpdateRequest() { }

        public AppointmentUpdateRequest(DateTime startDate, DateTime endDate, Doctor doctor)
        {
            _startDate = startDate;
            _endDate = endDate;
            _doctor = doctor;
        }
    }
}
