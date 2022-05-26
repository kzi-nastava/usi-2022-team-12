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
        #region helper classes
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
        #endregion

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

        private int _selectedSort;
        public int SelectedSort
        {
            get => _selectedSort;
            set
            {
                _selectedSort = value;
                UpdateDoctorInfoListView();
                OnPropertyChanged(nameof(SelectedSort));
            }
        }

        private int _selectedOrder;
        public int SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                UpdateDoctorInfoListView();
                OnPropertyChanged(nameof(SelectedOrder));
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
        #endregion

        #region methods
        public void UpdateDoctorInfoListView() {
            if (SelectedSort == 0)
            {
                if (SelectedOrder == 0)
                {
                    DoctorsInfo = DoctorsInfo.OrderBy(doc => doc.Doctor.FirstName).ToList<DoctorInfo>();
                }
                else
                {
                    DoctorsInfo = DoctorsInfo.OrderByDescending(doc => doc.Doctor.FirstName).ToList<DoctorInfo>();
                }
            }
            else if (SelectedSort == 1)
            {
                if (SelectedOrder == 0)
                {
                    DoctorsInfo = DoctorsInfo.OrderBy(doc => doc.Doctor.LastName).ToList<DoctorInfo>();
                }
                else
                {
                    DoctorsInfo = DoctorsInfo.OrderByDescending(doc => doc.Doctor.LastName).ToList<DoctorInfo>();
                }
            }
            else if (SelectedSort == 2)
            {
                if (SelectedOrder == 0)
                {
                    DoctorsInfo = DoctorsInfo.OrderBy(doc => doc.Doctor.Specialization).ToList<DoctorInfo>();
                }
                else
                {
                    DoctorsInfo = DoctorsInfo.OrderByDescending(doc => doc.Doctor.Specialization).ToList<DoctorInfo>();
                }
            }
            else if (SelectedSort == 3)
            {
                if (SelectedOrder == 0)
                {
                    DoctorsInfo = DoctorsInfo.OrderBy(doc => doc.AvgMark).ToList<DoctorInfo>();
                }
                else
                {
                    DoctorsInfo = DoctorsInfo.OrderByDescending(doc => doc.AvgMark).ToList<DoctorInfo>();
                }
            }
        }

        public void SearchDoctorInfo()
        {
            List<Doctor> doctors = new List<Doctor>();
            if (!string.IsNullOrEmpty(SearchText))
            {
                doctors = DoctorService.FilterDoctorsBySearchText(SearchText).ToList<Doctor>();
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
            SelectedSort = 0;
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
            SelectedSort = 0;
            SelectedOrder = 0;

            SearchDoctorInfoCommand = new SearchDoctorInfoCommand(this);
            AppointmentCreationCommand = new AppointmentCreationWithSelectedDoctorCommand(this);
        }
    }
}
