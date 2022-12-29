using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Core.Features.SurveyManagement.Commands.ManagerCMD;
using HealthInstitution.Core.Features.SurveyManagement.Model;
using HealthInstitution.Core.Features.SurveyManagement.Repository;
using HealthInstitution.Core.Features.SurveyManagement.Services;
using HealthInstitution.Core.Ninject;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.SurveyManagement
{
    public class SurveyOverviewViewModel : ViewModelBase
    {
        #region Attributes

        private IHealthInstitutionSurveyService _healthInstitutionSurveyService;
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

        #region Commands

        public ICommand? DoctorSurveyOverviewCommand { get; set; }

        #endregion

        #region Methods

        void LoadSurveys()
        {
            List<HealthInstitutionSurvey> HISurveys = ServiceLocator.Get<IHealthInstitutionSurveyRepository>().ReadAll().ToList();
            foreach (var survey in HISurveys)
            {
                _healthInstitutionSurveys.Add(survey);
            }
        }
        void LoadAverageRates()
        {
            _avgClearliness = _healthInstitutionSurveyService.AverageClearliness().ToString();
            _avgServiceQuality = _healthInstitutionSurveyService.AverageServiceQuality().ToString();
            _avgServiceSatisfaction = _healthInstitutionSurveyService.AverageServiceSatisfaction().ToString();
            _avgRecommendation = _healthInstitutionSurveyService.AverageRecommendation().ToString();
        }
        void LoadRates()
        {
            _rates = new List<int>();
            for (int i = 1; i <= 5; i++)
            {
                _rates.Add(i);
            }
            _selectedRate = _rates[0];
        }
        void LoadCategories()
        {
            _categories = new List<string>();
            _categories.Add("Clearliness");
            _categories.Add("Service quality");
            _categories.Add("Service satisfaction");
            _categories.Add("Recommendation");
            _selectedCategory = _categories[0];
        }

        #endregion

        public SurveyOverviewViewModel(IHealthInstitutionSurveyService healthInstitutionSurveyService)
        {
            _healthInstitutionSurveyService = healthInstitutionSurveyService;
            _healthInstitutionSurveys = new ObservableCollection<HealthInstitutionSurvey>();

            LoadSurveys();
            LoadAverageRates();
            LoadRates();
            LoadCategories();

            _rateNumBox = _healthInstitutionSurveyService.RatesPerSurveyCategory(_selectedRate, _selectedCategory);

            if (_healthInstitutionSurveys.Count != 0)
            {
                _selectedSurvey = _healthInstitutionSurveys[0];
                _commentBox = _selectedSurvey.Comment;
            }

            DoctorSurveyOverviewCommand = new DoctorSurveyOverviewCommand();

        }
    }
}
