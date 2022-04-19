namespace HealthInstitution.Model
{
    public class SurveyItem<T> : BaseObservableEntity
    {
        #region Attributes

        private string _itemDescription;
        public string ItemDescription { get => _itemDescription; set => OnPropertyChanged(ref _itemDescription, value); }

        private T _survey;
        public virtual T Survey { get => _survey; set => OnPropertyChanged(ref _survey, value); }

        #endregion

        #region Constructor

        public SurveyItem(string itemDescription, T survey)
        {
            _itemDescription = itemDescription;
            _survey = survey;
        }

        #endregion
    }
}
