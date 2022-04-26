
namespace HealthInstitution.Model
{
    public abstract class AppointmentRequest : BaseObservableEntity
    {
        #region Attributes

        private Patient _patient;
        public virtual Patient Patient { get => _patient; set => OnPropertyChanged(ref _patient, value); }

        private Appointment _appointment;
        public virtual Appointment Appointment { get => _appointment; set => OnPropertyChanged(ref _appointment, value); }

        private ActivityType _activityType;
        public ActivityType ActivityType { get => _activityType; set => OnPropertyChanged(ref _activityType, value); }

        private Status _status;
        public Status Status { get => _status; set => OnPropertyChanged(ref _status, value);}


        #endregion


        #region Constructor

        public AppointmentRequest() { }
        public AppointmentRequest(Patient patient, Appointment appointment, ActivityType activityType)
        {
            _patient = patient;
            _appointment = appointment;
            _activityType = activityType;
            _status = Status.Pending;
        }

        #endregion
    }
}
