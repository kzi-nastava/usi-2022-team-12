using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Commands.manager;
using HealthInstitution.Model.room;
using HealthInstitution.Model.survey;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;

namespace HealthInstitution.ViewModel.manager
{
    public class SurveyOverviewViewModel : ViewModelBase
    {
        #region Attributes
        private IHealthInstitutionSurveyService _healthInstitutionSurveyService;
        private IDoctorSurveyService _doctorSurveyService;
        private string _avgClearliness;
        private string _avgServiceQuality;
        private string _avgServiceSatisfaction;
        private string _avgRecommendation;
        private int _selectedRate;
        private string _selectedCategory;
        private int _rateNumBox;
        private HealthInstitutionSurvey _selectedSurvey;
        private string _commentBox;
        #endregion

        #region Properties

        public IHealthInstitutionSurveyService HealthInstitutionSurveyService
        {
            get => _healthInstitutionSurveyService;
        }
        public IDoctorSurveyService DoctorSurveyService
        {
            get => _doctorSurveyService;
        }
        public string AvgClearliness
        {
            get => _avgClearliness;
            set
            {
                _avgClearliness = value;
                OnPropertyChanged(nameof(AvgClearliness));
            }
        }
        public string AvgServiceQuality
        {
            get => _avgServiceQuality;
            set
            {
                _avgServiceQuality = value;
                OnPropertyChanged(nameof(AvgServiceQuality));
            }
        }
        public string AvgServiceSatisfaction
        {
            get => _avgServiceSatisfaction;
            set
            {
                _avgServiceSatisfaction = value;
                OnPropertyChanged(nameof(AvgServiceSatisfaction));
            }
        }
        public string AvgRecommendation
        {
            get => _avgRecommendation;
            set
            {
                _avgRecommendation = value;
                OnPropertyChanged(nameof(AvgRecommendation));
            }
        }
        public int SelectedRate
        {
            get => _selectedRate;
            set
            {
                _selectedRate = value;
                OnPropertyChanged(nameof(SelectedRate));
                _rateNumBox = _healthInstitutionSurveyService.RatesPerSurveyCategory(_selectedRate, _selectedCategory);
                OnPropertyChanged(nameof(RateNumBox));
            }
        }
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
                _rateNumBox = _healthInstitutionSurveyService.RatesPerSurveyCategory(_selectedRate, _selectedCategory);
                OnPropertyChanged(nameof(RateNumBox));
            }
        }
        public int RateNumBox
        {
            get => _rateNumBox;
            set
            {
                _rateNumBox = value;
                OnPropertyChanged(nameof(RateNumBox));
            }
        }
        public HealthInstitutionSurvey SelectedSurvey
        {
            get => _selectedSurvey;
            set
            {
                _selectedSurvey = value;
                OnPropertyChanged(nameof(SelectedSurvey));
                _commentBox = _selectedSurvey.Comment;
                OnPropertyChanged(nameof(CommentBox));
            }
        }

        public string CommentBox
        {
            get => _commentBox;
            set
            {
                _commentBox = value;
                OnPropertyChanged(nameof(CommentBox));
            }
        }
        #endregion

        #region Collections

        private readonly ObservableCollection<HealthInstitutionSurvey> _healthInstitutionSurveys;
        public IEnumerable<HealthInstitutionSurvey> HealthInstitutionSurveys => _healthInstitutionSurveys;

        private List<int> _rates;
        public List<int> Rates
        {
            get => _rates;
            set
            {
                _rates = value;
                OnPropertyChanged(nameof(Rates));
            }
        }

        private List<string> _categories;
        public List<string> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        #endregion

        public SurveyOverviewViewModel(IHealthInstitutionSurveyService healthInstitutionSurveyService, IDoctorSurveyService doctorSurveyService)
        {
            _healthInstitutionSurveyService = healthInstitutionSurveyService;
            _doctorSurveyService = doctorSurveyService;

            _healthInstitutionSurveys = new ObservableCollection<HealthInstitutionSurvey>();
            List<HealthInstitutionSurvey> HISurveys = _healthInstitutionSurveyService.ReadAll().ToList();
            foreach (var survey in HISurveys)
            {
                _healthInstitutionSurveys.Add(survey);
            }

            _avgClearliness = _healthInstitutionSurveyService.AverageClearliness().ToString();
            _avgServiceQuality = _healthInstitutionSurveyService.AverageServiceQuality().ToString();
            _avgServiceSatisfaction = _healthInstitutionSurveyService.AverageServiceSatisfaction().ToString();
            _avgRecommendation = _healthInstitutionSurveyService.AverageRecommendation().ToString();

            _rates = new List<int>();
            for (int i = 1; i <= 5; i++)
            {
                _rates.Add(i);
            }
            _selectedRate = _rates[0];

            _categories = new List<string>();
            _categories.Add("Clearliness");
            _categories.Add("Service quality");
            _categories.Add("Service satisfaction");
            _categories.Add("Recommendation");
            _selectedCategory = _categories[0];

            _rateNumBox = _healthInstitutionSurveyService.RatesPerSurveyCategory(_selectedRate, _selectedCategory);

            if (_healthInstitutionSurveys.Count != 0)
            {
                _selectedSurvey = _healthInstitutionSurveys[0];
                _commentBox = _selectedSurvey.Comment;
            }
            
        }
    }
}
