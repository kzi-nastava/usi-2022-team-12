namespace HealthInstitution.Model.appointment
{
    public class Illness : BaseObservableEntity
    {
        #region Attributes

        private string _name;

        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        #endregion

        #region Constructor
        public Illness() { }
        public Illness(string name)
        {
            Name = name;
        }
        #endregion
    }
}
