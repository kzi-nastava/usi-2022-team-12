using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Core.Features.SurveyManagement.Commands.ManagerCMD;
using HealthInstitution.Core.Features.SurveyManagement.Model;
using HealthInstitution.Core.Features.SurveyManagement.Repository;
using HealthInstitution.Core.Features.SurveyManagement.Services;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Service;
using HealthInstitution.GUI.Utility.ViewModel;
using static HealthInstitution.GUI.Features.UsersManagement.DoctorSearchViewModel;

namespace HealthInstitution.GUI.Features.SurveyManagement
{
    public class DoctorSurveyOverviewViewModel : ViewModelBase
    {
        #region Attributes

        private IDoctorSurveyRepository _doctorSurveyRepository;
        private IDoctorSurveyService _doctorSurveyService;
        private IDoctorRepository _doctorRepository;
        private Doctor _selectedDoctor;
        private string _avgServiceQuality;
        private string _avgRecommendation;
        private int _selectedRate;
        private string _selectedCategory;
        private int _rateNumBox;
        private List<DoctorInfo> _doctorsInfo;
        private List<DoctorInfo> _bestDoctorsInfo;
        private List<DoctorInfo> _worstDoctorsInfo;
        private List<DoctorInfo> _selectedDoctorsInfo;
        private string _selectedCriteria;

        #endregion

        #region Properties

        public IDoctorSurveyService DoctorSurveyService
        {
            get => _doctorSurveyService;
        }
        public IDoctorRepository DoctorRepository
        {
            get => _doctorRepository;
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
        public List<DoctorInfo> DoctorsInfo
        {
            get => _doctorsInfo;
            set
            {
                _doctorsInfo = value;
                OnPropertyChanged(nameof(DoctorsInfo));
            }
        }
        public List<DoctorInfo> BestDoctorsInfo
        {
            get => _bestDoctorsInfo;
            set
            {
                _bestDoctorsInfo = value;
                OnPropertyChanged(nameof(BestDoctorsInfo));
            }
        }
        public List<DoctorInfo> WorstDoctorsInfo
        {
            get => _worstDoctorsInfo;
            set
            {
                _worstDoctorsInfo = value;
                OnPropertyChanged(nameof(WorstDoctorsInfo));
            }
        }

        public List<DoctorInfo> SelectedDoctorsInfo
        {
            get => _selectedDoctorsInfo;
            set
            {
                _selectedDoctorsInfo = value;
                OnPropertyChanged(nameof(SelectedDoctorsInfo));
            }
        }
        public string SelectedCriteria
        {
            get => _selectedCriteria;
            set
            {
                _selectedCriteria = value;
                OnPropertyChanged(nameof(SelectedCriteria));
                if (_selectedCriteria == "Best")
                {
                    _selectedDoctorsInfo = _bestDoctorsInfo;
                    OnPropertyChanged(nameof(SelectedDoctorsInfo));
                }
                else
                {
                    _selectedDoctorsInfo = _worstDoctorsInfo;
                    OnPropertyChanged(nameof(SelectedDoctorsInfo));
                }
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
        private List<string> _criteria;
        public List<string> Criteria
        {
            get => _criteria;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Criteria));
            }
        }

        #endregion

        #region Commands

        public ICommand? SurveyOverviewCommand { get; set; }

        #endregion

        #region Methods

        void LoadSurveys()
        {
            List<DoctorSurvey> DocSurveys = _doctorSurveyRepository.ReadAll().ToList();
            foreach (var survey in DocSurveys)
            {
                _doctorSurveys.Add(survey);
            }
        }

        void LoadDoctors()
        {
            _doctors = _doctorRepository.ReadAll().ToList();
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

        void LoadCriteria()
        {
            _criteria = new List<string>();
            _criteria.Add("Best");
            _criteria.Add("Worst");
            _selectedCriteria = _criteria[0];
        }

        void LoadBestAndWorstDoctors()
        {
            List<Doctor> doctors = _doctorSurveyService.RatedDoctors();
            //List<Doctor> doctors = _doctorService.ReadAll().ToList();
            _doctorsInfo = new List<DoctorInfo>();
            foreach (Doctor doctor in doctors)
            {
                double avgMark = Math.Round(_doctorSurveyService.CalculateAvgMark(doctor), 2);
                _doctorsInfo.Add(new DoctorInfo() { Doctor = doctor, AvgMark = avgMark });
            }
            _doctorsInfo.Sort((x, y) => x.AvgMark.CompareTo(y.AvgMark));
            _bestDoctorsInfo = new List<DoctorInfo>();
            _worstDoctorsInfo = new List<DoctorInfo>();
            for (int i = 0; i < _doctorsInfo.Count; i++)
            {
                if (i > 2) break;
                _worstDoctorsInfo.Add(_doctorsInfo[i]);
                _bestDoctorsInfo.Add(_doctorsInfo[_doctorsInfo.Count - 1 - i]);
            }
            _selectedDoctorsInfo = _bestDoctorsInfo;
        }

        #endregion

        public DoctorSurveyOverviewViewModel(IDoctorSurveyService doctorSurveyService, IDoctorSurveyRepository doctorSurveyRepository, IDoctorRepository doctorRepository)
        {
            _doctorSurveyService = doctorSurveyService;
            _doctorSurveyRepository = doctorSurveyRepository;
            _doctorRepository = doctorRepository;
            _doctorSurveys = new ObservableCollection<DoctorSurvey>();

            LoadSurveys();
            LoadRates();
            LoadCategories();
            LoadDoctors();
            LoadBestAndWorstDoctors();
            LoadCriteria();


            SurveyOverviewCommand = new SurveyOverviewCommand();
        }
    }
}
