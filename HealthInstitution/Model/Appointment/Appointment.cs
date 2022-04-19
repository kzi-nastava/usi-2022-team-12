using System;

namespace HealthInstitution.Model
{
    public class Appointment : BaseObservableEntity
    {
        #region Attributes

        private Doctor _doctor;
        public virtual Doctor Doctor { get => _doctor; set => OnPropertyChanged(ref _doctor, value); }

        private Patient _patient;
        public virtual Patient Patient { get => _patient; set => OnPropertyChanged(ref _patient, value); }

        private DateTime _startDate;
        public DateTime StartDate { get => _startDate; set => OnPropertyChanged(ref _startDate, value); }

        private DateTime _endDate;
        public DateTime EndDate { get => _endDate; set => OnPropertyChanged(ref _endDate, value); }

        private Room _room;
        public virtual Room Room { get => _room; set => OnPropertyChanged(ref _room, value); }

        private Anamnesis _anamnesis;
        public virtual Anamnesis Anamnesis { get => _anamnesis; set => OnPropertyChanged(ref _anamnesis, value); }

        #endregion

        #region Constructor

        public Appointment ()
        {

        }

        public Appointment(Doctor doctor, Patient patient, DateTime startDate, Room room, Anamnesis anamnesis)
        {
            _doctor = doctor;
            _patient = patient;
            _startDate = startDate;
            _endDate = startDate.AddMinutes(15);
            _room = room;
            _anamnesis = anamnesis;
        }

        #endregion
    }
}
