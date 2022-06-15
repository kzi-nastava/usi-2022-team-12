using System.Windows.Input;
using HealthInstitution.Core.Features.SurveyManagement.Commands.PatientCMD;
using HealthInstitution.Core.Features.SurveyManagement.Repository;
using HealthInstitution.Core.Features.SurveyManagement.Services;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.SurveyManagement
{
    public class HealthInstitutionSurveyViewModel : ViewModelBase
    {
        #region services
        private readonly IHealthInstitutionSurveyService _healthInstitutionSurveyService;
        private readonly IHealthInstitutionSurveyRepository _healthInstitutionSurveyRepository;
        public IHealthInstitutionSurveyService HealthInstitutionSurveyService => _healthInstitutionSurveyService;
        public IHealthInstitutionSurveyRepository HealthInstitutionSurveyRepository => _healthInstitutionSurveyRepository;
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

        public HealthInstitutionSurveyViewModel(IHealthInstitutionSurveyService healthInstitutionSurveyService, IHealthInstitutionSurveyRepository healthInstitutionSurveyRepository)
        {
            _healthInstitutionSurveyService = healthInstitutionSurveyService;
            _healthInstitutionSurveyRepository = healthInstitutionSurveyRepository;
            ServiceQualityRating = 1;
            RecommendationRating = 1;
            CleanlinessRating = 1;
            ServiceSatisfactionRating = 1;

            FinishHealthInstitutionSurveyCommand = new FinishHealthInstitutionSurveyCommand(this);
        }
    }
}
