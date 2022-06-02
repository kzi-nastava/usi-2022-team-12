using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class PatientMedicalRecordViewModel : ViewModelBase
    {
        #region services
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly IAppointmentService _appointmentService;

        public IMedicalRecordService MedicalRecordService => _medicalRecordService;
        public IAppointmentService AppointmentService => _appointmentService;
        #endregion

        #region attributes
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

        public string Height => MedicalRecord.Height.ToString();
        public string Age => CalculateAge(Patient.DateOfBirth).ToString();
        public string Weight => MedicalRecord.Weight.ToString();

        private List<Illness> _illnessHistory;
        public List<Illness> IllnessHistory
        {
            get => _illnessHistory;
            set
            {
                _illnessHistory = value;
                OnPropertyChanged(nameof(IllnessHistory));
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

        private string _anamnesisSearchCriteria;
        public string AnamnesisSearchCriteria
        {
            get => _anamnesisSearchCriteria;
            set
            {
                _anamnesisSearchCriteria = value;
                OnPropertyChanged(nameof(AnamnesisSearchCriteria));
            }
        }

        private int _selectedSort;
        public int SelectedSort
        {
            get => _selectedSort;
            set
            {
                _selectedSort = value;
                UpdatePastAppointmentsListView();
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
                UpdatePastAppointmentsListView();
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }
        #endregion

        #region commands
        public ICommand SearchByAnamnesisCommand { get; }
        public ICommand OpenDoctorSurveyCommand { get; }

        #endregion

        #region methods
        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Subtract(dateOfBirth).Days;
            age = age / 365;
            return age;
        }

        public void SearchByAnamnesis()
        {
            if (!string.IsNullOrEmpty(AnamnesisSearchCriteria))
            {
                PastAppointments = AppointmentService.FindFinishedAppointmentsWithAnamnesis(Patient, AnamnesisSearchCriteria).ToList<Appointment>();
            }
            else
            {
                PastAppointments = AppointmentService.ReadFinishedAppointmentsForPatient(Patient).ToList<Appointment>();
            }
            SelectedSort = 0;
            SelectedOrder = 0;
        }


        public void UpdatePastAppointmentsListView()
        {
            if (SelectedSort == 0)
            {
                if (SelectedOrder == 0)
                {
                    PastAppointments = PastAppointments.OrderBy(apt => apt.StartDate).ToList<Appointment>();
                }
                else
                {
                    PastAppointments = PastAppointments.OrderByDescending(apt => apt.StartDate).ToList<Appointment>();
                }
            }
            else if (SelectedSort == 1)
            {
                if (SelectedOrder == 0)
                {
                    PastAppointments = PastAppointments.OrderBy(apt => apt.Doctor.FullName).ToList<Appointment>();
                }
                else
                {
                    PastAppointments = PastAppointments.OrderByDescending(apt => apt.Doctor.FullName).ToList<Appointment>();
                }
            }
            else if (SelectedSort == 2)
            {
                if (SelectedOrder == 0)
                {
                    PastAppointments = PastAppointments.OrderBy(apt => apt.Doctor.Specialization).ToList<Appointment>();
                }
                else
                {
                    PastAppointments = PastAppointments.OrderByDescending(apt => apt.Doctor.Specialization).ToList<Appointment>();
                }
            }
        }
        #endregion

        public PatientMedicalRecordViewModel(IMedicalRecordService medicalRecordService, IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
            _medicalRecordService = medicalRecordService;

            _selectedSort = 0;
            _selectedOrder = 0;
            _patient = GlobalStore.ReadObject<Patient>("LoggedUser");
            _medicalRecord = medicalRecordService.GetMedicalRecordForPatient(Patient);
            _illnessHistory = _medicalRecord.IllnessHistory.ToList<Illness>();
            _allergens = _medicalRecord.Allergens.ToList<Allergen>();
            _pastAppointments = appointmentService.ReadFinishedAppointmentsForPatient(Patient).ToList<Appointment>();

            SearchByAnamnesisCommand = new SearchByAnamnesisCommand(this);
            OpenDoctorSurveyCommand = new OpenDoctorSurveyCommand(this);
        }
    }
}
