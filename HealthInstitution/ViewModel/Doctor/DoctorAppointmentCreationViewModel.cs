using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class DoctorAppointmentCreationViewModel : ViewModelBase, IDoctorAppointmentViewModel
    {
        private IPatientService _patientService;

        private IAppointmentService _appointmentService;
        public IAppointmentService AppointmentService => _appointmentService;

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private DateTime _time;
        public DateTime Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }
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

        private ObservableCollection<Patient> _patients;
        public IEnumerable<Patient> Patients
        {
            get => _patients;
            set
            {
                _patients = new ObservableCollection<Patient>();
                foreach (var patient in value)
                {
                    _patients.Add(patient);
                }
                OnPropertyChanged(nameof(Patients));
            }
        }
        public ICommand CancelAppointmentCreationCommand { get; }
        public ICommand CreateAppointmentCommand { get; }
        public ICommand SearchPatientCommand { get; }
        public DoctorAppointmentCreationViewModel(IPatientService patientService, IAppointmentService appointmentService)
        {
            _date = DateTime.Now.AddDays(1);
            _appointmentService = appointmentService;
            _patientService = patientService;
            UpdateData(null);
            CancelAppointmentCreationCommand = new NavigateScheduleCommand();
            CreateAppointmentCommand = new CreateAppointmentCommand(this);
            SearchPatientCommand = new SearchPatientsCommand(this);
        }
        public void UpdateData(string prefix)
        {
            _patients = new ObservableCollection<Patient>();
            IEnumerable<Patient> patients = null;
            if (string.IsNullOrEmpty(prefix))
            {
                patients = _patientService.ReadAll();
            }
            else
            {
                patients = _patientService.FilterValidPatientsBySearchText(prefix);
            }
            foreach (var patient in patients)
            {
                _patients.Add(patient);
            }
            OnPropertyChanged(nameof(Patients));
        }

    }
}
