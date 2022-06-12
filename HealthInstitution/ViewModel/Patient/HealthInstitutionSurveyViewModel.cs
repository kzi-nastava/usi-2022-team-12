using System.Windows.Input;
using HealthInstitution.Commands.patient;
using HealthInstitution.Services.Intefaces;

namespace HealthInstitution.ViewModel.patient
{
    public class HealthInstitutionSurveyViewModel : ViewModelBase
    {
        #region services
        private readonly IHealthInstitutionSurveyService _healthInstitutionSurveyService;
        public IHealthInstitutionSurveyService HealthInstitutionSurveyService => _healthInstitutionSurveyService;
        #endregion

        #region attributes
        private int _serviceQualityRating;
        private int _cleanlinessRating;
        private int _serviceSatisfactionRating;
        private int _recommendationRating;
        private string _comment;
        #endregion

        #region properties
        public int ServiceQualityRating
        {
            get => _serviceQualityRating;
            set
            {
                _serviceQualityRating = value;
                OnPropertyChanged(nameof(ServiceQualityRating));
            }
        }

        public int CleanlinessRating
        {
            get => _cleanlinessRating;
            set
            {
                _cleanlinessRating = value;
                OnPropertyChanged(nameof(CleanlinessRating));
            }
        }
        public int ServiceSatisfactionRating
        {
            get => _serviceSatisfactionRating;
            set
            {
                _serviceSatisfactionRating = value;
                OnPropertyChanged(nameof(ServiceSatisfactionRating));
            }
        }
        public int RecommendationRating
        {
            get => _recommendationRating;
            set
            {
                _recommendationRating = value;
                OnPropertyChanged(nameof(RecommendationRating));
            }
        }
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        #endregion

        #region commands
        public ICommand FinishHealthInstitutionSurveyCommand { get; }
        public ICommand BackCommand { get; }
        #endregion

        public HealthInstitutionSurveyViewModel(IHealthInstitutionSurveyService healthInstitutionSurveyService)
        {
            _healthInstitutionSurveyService = healthInstitutionSurveyService;
            ServiceQualityRating = 1;
            RecommendationRating = 1;
            CleanlinessRating = 1;
            ServiceSatisfactionRating = 1;

            FinishHealthInstitutionSurveyCommand = new FinishHealthInstitutionSurveyCommand(this);
        }
    }
}
