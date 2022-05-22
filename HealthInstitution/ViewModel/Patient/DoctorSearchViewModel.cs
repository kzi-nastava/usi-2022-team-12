using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class DoctorSearchViewModel : ViewModelBase
    {
        public class DoctorInfo
        {
            private Doctor _doctor;
            public Doctor Doctor
            {
                get { return _doctor; } 
                set { _doctor = value; } 
            }

            private double _avgMark;
            public double AvgMark
            {
                get { return _avgMark; }
                set { _avgMark = value; }
            }
        }

        #region services
        private readonly IDoctorService _doctorService;
        private readonly IDoctorMarkService _doctorMarkService;

        public IDoctorService DoctorService => _doctorService;
        public IDoctorMarkService DoctorMarkService => _doctorMarkService;
        #endregion

        #region attributes
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        private string _selectedSearch;
        public string SelectedSearch
        {
            get => _selectedSearch;
            set
            {
                _selectedSearch = value;
                SelectedSort = "";
                OnPropertyChanged(nameof(SelectedSearch));
            }
        }

        private string _selectedSort;
        public string SelectedSort
        {
            get => _selectedSort;
            set
            {
                _selectedSort = value;

                if (SelectedSort.Equals("SearchCriteria"))
                {

                    if (SelectedSearch.Equals("FirstName"))
                    {
                        DoctorsInfo = DoctorsInfo.OrderBy(doc => doc.Doctor.FirstName).ToList<DoctorInfo>();
                    }
                    else if (SelectedSearch.Equals("LastName"))
                    {
                        DoctorsInfo = DoctorsInfo.OrderBy(doc => doc.Doctor.LastName).ToList<DoctorInfo>();
                    }
                    else if (SelectedSearch.Equals("Specialization"))
                    {
                        DoctorsInfo = DoctorsInfo.OrderBy(doc => doc.Doctor.Specialization).ToList<DoctorInfo>();
                    }
                }
                else if (SelectedSort.Equals("Marks"))
                {
                    DoctorsInfo = DoctorsInfo.OrderByDescending(doc => doc.AvgMark).ToList<DoctorInfo>();
                }

                OnPropertyChanged(nameof(SelectedSort));
            }
        }

        private DoctorInfo _selectedDoctorInfo;
        public DoctorInfo SelectedDoctorInfo
        {
            get => _selectedDoctorInfo;
            set
            {
                _selectedDoctorInfo = value;
                OnPropertyChanged(nameof(SelectedDoctorInfo));
            }
        }

        private List<DoctorInfo> _doctorsInfo;
        public List<DoctorInfo> DoctorsInfo
        {
            get => _doctorsInfo;
            set
            {
                _doctorsInfo = value;
                OnPropertyChanged(nameof(DoctorsInfo));
            }
        }
        #endregion

        #region commands
        public ICommand SearchDoctorInfoCommand { get; }
        public ICommand AppointmentCreationCommand { get; }
        public ICommand PatientAppointmentsCommand { get; }
        #endregion

        #region methods
        public void SearchDoctorInfo()
        {
            List<Doctor> doctors = new List<Doctor>();
            if (!string.IsNullOrEmpty(SearchText))
            {
                if (SelectedSearch.Equals("FirstName"))
                {
                    doctors = DoctorService.SearchDoctorsWithFirstName(SearchText).ToList<Doctor>();
                }
                else if (SelectedSearch.Equals("LastName"))
                {
                    doctors = DoctorService.SearchDoctorsWithLastName(SearchText).ToList<Doctor>();
                }
                else if (SelectedSearch.Equals("Specialization"))
                {
                    doctors = DoctorService.SearchDoctorsWithSpecializationName(SearchText).ToList<Doctor>();
                }
            }
            else
            {
                doctors = DoctorService.ReadAll().ToList<Doctor>();
            }

            List<DoctorInfo> doctorsInfo = new List<DoctorInfo>();
            foreach (Doctor doctor in doctors)
            {
                double avgMark = Math.Round(DoctorMarkService.CalculateAvgMark(doctor), 2);
                doctorsInfo.Add(new DoctorInfo() { Doctor = doctor, AvgMark = avgMark });
            }

            DoctorsInfo = doctorsInfo;
            SelectedSort = "";
        }
        #endregion
        public DoctorSearchViewModel(IDoctorService doctorService, IDoctorMarkService doctorMarkService) {
            _doctorService = doctorService;
            _doctorMarkService = doctorMarkService;

            DoctorsInfo = new List<DoctorInfo>();
            var doctors = DoctorService.ReadAll().ToList<Doctor>();
            foreach (Doctor doctor in doctors) {
                double avgMark = Math.Round(DoctorMarkService.CalculateAvgMark(doctor), 2);
                DoctorsInfo.Add(new DoctorInfo() { Doctor = doctor, AvgMark = avgMark });
            }
            SelectedSearch = "FirstName";
            SelectedSort = "";

            SearchDoctorInfoCommand = new SearchDoctorInfoCommand(this);
            AppointmentCreationCommand = new AppointmentCreationWithSelectedDoctorCommand(this);
            PatientAppointmentsCommand = new PatientAppointmentsCommand();
        }
    }
}
