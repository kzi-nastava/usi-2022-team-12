using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.MedicalRecordManagement.Commands.PatientCMD;
using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Features.MedicalRecordManagement.Repository;
using HealthInstitution.Core.Features.SurveyManagement.Commands.PatientCMD;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.MedicalRecordManagement
{
    public class PatientMedicalRecordViewModel : ViewModelBase
    {
        #region services
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        private readonly ISchedulingService _schedulingService;

        public IMedicalRecordRepository MedicalRecordRepository => _medicalRecordRepository;
        public ISchedulingService SchedulingService => _schedulingService;
        #endregion

        #region attributes
        private MedicalRecord _medicalRecord;
        private Patient _patient;
        private List<Illness> _illnessHistory;
        private List<Allergen> _allergens;
        private List<Appointment> _pastAppointments;
        private Appointment _selectedAppointment;
        private string _anamnesisSearchCriteria;
        private int _selectedSort;
        private int _selectedOrder;
        #endregion

        #region properties
        public string Height => MedicalRecord.Height.ToString();
        public string Age => CalculateAge(Patient.DateOfBirth).ToString();
        public string Weight => MedicalRecord.Weight.ToString();

        public MedicalRecord MedicalRecord
        {
            get => _medicalRecord;
            set
            {
                _medicalRecord = value;
                OnPropertyChanged(nameof(MedicalRecord));
            }
        }
        public Patient Patient
        {
            get => _patient;
            set
            {
                _patient = value;
                OnPropertyChanged(nameof(Patient));
            }
        }
        public List<Illness> IllnessHistory
        {
            get => _illnessHistory;
            set
            {
                _illnessHistory = value;
                OnPropertyChanged(nameof(IllnessHistory));
            }
        }
        public List<Allergen> Allergens
        {
            get => _allergens;
            set
            {
                _allergens = value;
                OnPropertyChanged(nameof(Allergens));
            }
        }
        public List<Appointment> PastAppointments
        {
            get => _pastAppointments;
            set
            {
                _pastAppointments = value;
                OnPropertyChanged(nameof(PastAppointments));
            }
        }
        public Appointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                _selectedAppointment = value;
                OnPropertyChanged(nameof(SelectedAppointment));
            }
        }
        public string AnamnesisSearchCriteria
        {
            get => _anamnesisSearchCriteria;
            set
            {
                _anamnesisSearchCriteria = value;
                OnPropertyChanged(nameof(AnamnesisSearchCriteria));
            }
        }
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
                PastAppointments = SchedulingService.FindFinishedAppointmentsWithAnamnesis(Patient, AnamnesisSearchCriteria).ToList<Appointment>();
            }
            else
            {
                PastAppointments = SchedulingService.ReadFinishedAppointmentsForPatient(Patient).ToList<Appointment>();
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

        public PatientMedicalRecordViewModel(IMedicalRecordRepository medicalRecordRepository, ISchedulingService schedulingService)
        {
            _schedulingService = schedulingService;
            _medicalRecordRepository = medicalRecordRepository;

            _selectedSort = 0;
            _selectedOrder = 0;
            _patient = GlobalStore.ReadObject<Patient>("LoggedUser");
            _medicalRecord = _medicalRecordRepository.GetMedicalRecordForPatient(Patient);
            _illnessHistory = _medicalRecord.IllnessHistory.ToList<Illness>();
            _allergens = _medicalRecord.Allergens.ToList<Allergen>();
            _pastAppointments = _schedulingService.ReadFinishedAppointmentsForPatient(Patient).ToList<Appointment>();

            SearchByAnamnesisCommand = new SearchByAnamnesisCommand(this);
            OpenDoctorSurveyCommand = new OpenDoctorSurveyCommand(this);
        }
    }
}
