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
    public class PatientMedicalRecordViewModel : ViewModelBase
    {
        public IMedicalRecordService medicalRecordService;

        public IAppointmentService appointmentService;


        private MedicalRecord _medicalRecord;
        public MedicalRecord MedicalRecord
        {
            get => _medicalRecord;
            set
            {
                _medicalRecord = value;
                OnPropertyChanged(nameof(MedicalRecord));
            }
        }

        private Patient _patient;
        public Patient Patient
        {
            get => _patient;
            set
            {
                _patient = value;
                OnPropertyChanged(nameof(Patient));
            }
        }

        public string Height => _medicalRecord.Height.ToString();
        public string Age => CalculateAge(_patient.DateOfBirth).ToString();
        public string Weight => _medicalRecord.Weight.ToString();

        private List<Illness> _illnessHistoryData;
        public List<Illness> IllnessHistoryData
        {
            get => _illnessHistoryData;
            set
            {
                _illnessHistoryData = value;
                OnPropertyChanged(nameof(IllnessHistoryData));
            }
        }

        private List<Allergen> _allergens;
        public List<Allergen> Allergens
        {
            get => _allergens;
            set
            {
                _allergens = value;
                OnPropertyChanged(nameof(Allergens));
            }
        }

        private List<Appointment> _pastAppointments;
        public List<Appointment> PastAppointments
        {
            get => _pastAppointments;
            set
            {
                _pastAppointments = value;
                OnPropertyChanged(nameof(PastAppointments));
            }
        }

        private Appointment _selectedAppointment;
        public Appointment SelectedAppointment 
        {
            get => _selectedAppointment;
            set 
            {
                _selectedAppointment = value;
                OnPropertyChanged(nameof(SelectedAppointment));
            }
        }

        private string _searchByAnamnesisText;
        public string SearchByAnamnesisText
        {
            get => _searchByAnamnesisText;
            set
            {
                _searchByAnamnesisText = value;
                OnPropertyChanged(nameof(SearchByAnamnesisText));
            }
        }

        private string _selectedSort;
        public string SelectedSort
        {
            get => _selectedSort;
            set
            {
                _selectedSort = value;

                if (SelectedSort.Equals("Date"))
                {
                    PastAppointments = PastAppointments.OrderBy(apt => apt.StartDate).ToList<Appointment>();
                }
                else if (SelectedSort.Equals("Doctor"))
                {
                    PastAppointments = PastAppointments.OrderBy(apt => apt.Doctor.FullName).ToList<Appointment>();
                }
                else if (SelectedSort.Equals("Specialization"))
                {
                    PastAppointments = PastAppointments.OrderBy(apt => apt.Doctor.Specialization).ToList<Appointment>();
                }
                OnPropertyChanged(nameof(SelectedSort));
            }
        }

        public ICommand SearchByAnamnesisCommand { get; }

        public PatientMedicalRecordViewModel(IMedicalRecordService medicalRecordService, IAppointmentService appointmentService)
        {
            SearchByAnamnesisCommand = new SearchByAnamnesisCommand(this);
            this.appointmentService = appointmentService;
            this.medicalRecordService = medicalRecordService;
            SelectedSort = "";
            Patient = GlobalStore.ReadObject<Patient>("LoggedUser");
            _medicalRecord = medicalRecordService.GetMedicalRecordForPatient(Patient);
            IllnessHistoryData = _medicalRecord.IllnessHistory.ToList<Illness>();
            Allergens = _medicalRecord.Allergens.ToList<Allergen>();
            PastAppointments = appointmentService.ReadFinishedAppointmentsForPatient(Patient).ToList<Appointment>();
        }

        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Subtract(dateOfBirth).Days;
            age = age / 365;
            return age;
        }

        public void SearchByAnamnesis(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                PastAppointments = appointmentService.FilterFinishedAppointmentsByAnamnesisSearchText(text, Patient).ToList<Appointment>();
            }
            else {
                PastAppointments = appointmentService.ReadFinishedAppointmentsForPatient(Patient).ToList<Appointment>();
            }
            SelectedSort = "";
        }
    }
}
