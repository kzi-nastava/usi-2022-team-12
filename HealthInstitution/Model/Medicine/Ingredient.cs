namespace HealthInstitution.Model
{
    public class Ingredient : BaseObservableEntity
    {
        #region Attributes

        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        #endregion

        #region Constructor

        public Ingredient()
        {

        }

        public Ingredient(string name)
        {
            _name = name;
        }

        #endregion
    }
}
