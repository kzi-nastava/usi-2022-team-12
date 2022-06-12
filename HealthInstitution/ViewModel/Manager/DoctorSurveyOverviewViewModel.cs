using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Commands.manager;
using HealthInstitution.Model.room;
using HealthInstitution.Model.survey;
using HealthInstitution.Model.user;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;

namespace HealthInstitution.ViewModel.manager
{
    public class DoctorSurveyOverviewViewModel : ViewModelBase
    {
        #region Attributes

        private IDoctorSurveyService _doctorSurveyService;
        private IDoctorService _doctorService;
        private Doctor _selectedDoctor;
        private string _avgServiceQuality;
        private string _avgRecommendation;
        private int _selectedRate;
        private string _selectedCategory;
        private int _rateNumBox;

        #endregion

        #region Properties

        public IDoctorSurveyService DoctorSurveyService
        {
            get => _doctorSurveyService;
        }
        public IDoctorService DoctorService
        {
            get => _doctorService;
        }
        public Doctor SelectedDoctor
        {
            get => _selectedDoctor;
            set
            {
                _selectedDoctor = value;
                OnPropertyChanged(nameof(SelectedDoctor));
                LoadAverage(_selectedDoctor);
                OnPropertyChanged(nameof(AvgServiceQuality));
                OnPropertyChanged(nameof(AvgRecommendation));
                _rateNumBox = _doctorSurveyService.RatesPerSurveyCategory(_selectedRate, _selectedCategory, _selectedDoctor);
                OnPropertyChanged(nameof(RateNumBox));
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
                _rateNumBox = _doctorSurveyService.RatesPerSurveyCategory(_selectedRate, _selectedCategory, _selectedDoctor);
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
                _rateNumBox = _doctorSurveyService.RatesPerSurveyCategory(_selectedRate, _selectedCategory, _selectedDoctor);
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

        #endregion

        #region Collections

        private readonly ObservableCollection<DoctorSurvey> _doctorSurveys;
        public IEnumerable<DoctorSurvey> DoctorSurveys => _doctorSurveys;
        private List<Doctor> _doctors;
        public List<Doctor> Doctors
        {
            get => _doctors;
            set
            {
                _doctors = value;
                OnPropertyChanged(nameof(Doctors));
            }
        }
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

        public ICommand? SurveyOverviewCommand { get; set; }

        #endregion

        #region Methods

        void LoadSurveys()
        {
            List<DoctorSurvey> DocSurveys = _doctorSurveyService.ReadAll().ToList();
            foreach (var survey in DocSurveys)
            {
                _doctorSurveys.Add(survey);
            }
        }

        void LoadDoctors()
        {
            _doctors = _doctorService.ReadAll().ToList();
            _doctors = _doctors.OrderBy(x => x.FirstName).ToList();
            if (_doctors.Count() != 0)
            {
                _selectedDoctor = _doctors[0];
                LoadAverage(_selectedDoctor);
                _rateNumBox = _doctorSurveyService.RatesPerSurveyCategory(_selectedRate, _selectedCategory, _selectedDoctor);
            }
        }

        void LoadAverage(Doctor doc)
        {
            _avgServiceQuality = _doctorSurveyService.AverageServiceQuality(doc).ToString();
            _avgRecommendation = _doctorSurveyService.AverageRecommendation(doc).ToString();
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
            _categories.Add("Service quality");
            _categories.Add("Recommendation");
            _selectedCategory = _categories[0];
        }

        #endregion

        public DoctorSurveyOverviewViewModel(IDoctorSurveyService doctorSurveyService, IDoctorService doctorService)
        {
            _doctorSurveyService = doctorSurveyService;
            _doctorService = doctorService;
            _doctorSurveys = new ObservableCollection<DoctorSurvey>();

            LoadSurveys();
            LoadRates();
            LoadCategories();
            LoadDoctors();
            

            SurveyOverviewCommand = new SurveyOverviewCommand();
        }
    }
}
