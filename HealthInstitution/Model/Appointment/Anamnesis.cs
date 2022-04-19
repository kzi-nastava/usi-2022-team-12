namespace HealthInstitution.Model
{
    public class Anamnesis : BaseObservableEntity
    {
        #region Attributes

        private string _description;
        public string Description { get => _description; set => OnPropertyChanged(ref _description, value); }

        #endregion

        #region Constructor

        public Anamnesis(string description)
        {
            Description = description;
        }

        #endregion
    }
}
