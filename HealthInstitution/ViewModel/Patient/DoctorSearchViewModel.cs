using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
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

            private double _avgRating;
            public double AvgRating
            {
                get { return _avgRating; }
                set { _avgRating = value; }
            }
        }

        #region services
        private readonly IDoctorService _doctorService;

        public IDoctorService DoctorService => _doctorService;
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
        public ICommand PatientAppointmentsCommand { get; }
        #endregion
        public DoctorSearchViewModel(IDoctorService doctorService) {
            _doctorService = doctorService;
            DoctorsInfo = new List<DoctorInfo>();
            var doctors = doctorService.ReadAll().ToList<Doctor>();
            foreach (Doctor doctor in doctors) {
                DoctorsInfo.Add(new DoctorInfo() { Doctor = doctor, AvgRating = 2 });
            }

            PatientAppointmentsCommand = new PatientAppointmentsCommand();
        }
    }
}
