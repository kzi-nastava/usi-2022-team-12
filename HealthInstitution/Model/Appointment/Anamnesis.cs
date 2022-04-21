namespace HealthInstitution.Model
{
    public class Anamnesis : BaseObservableEntity
    {
        #region Attributes
        private Appointment _appointment;
        public Appointment? Appointment { get => _appointment; set => OnPropertyChanged(ref _appointment, value); }

        private string _description;
        public string Description { get => _description; set => OnPropertyChanged(ref _description, value); }

        #endregion

        #region Constructor

        public Anamnesis(string description, Appointment appointment)
        {
            Description = description;
            Appointment = appointment;
        }

        #endregion
    }
}
