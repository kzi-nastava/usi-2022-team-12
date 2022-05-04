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

        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Subtract(dateOfBirth).Days;
            age = age / 365;
            return age;
        }

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
        public ICommand BackCommand { get; }

        public PatientMedicalRecordViewModel(IMedicalRecordService medicalRecordService, IAppointmentService appointmentService)
        {
            //BackCommand = new NavigateScheduleCommand();
            this.appointmentService = appointmentService;
            this.medicalRecordService = medicalRecordService;
            Patient = GlobalStore.ReadObject<Patient>("LoggedUser");
            _medicalRecord = medicalRecordService.GetMedicalRecordForPatient(Patient);
            IllnessHistoryData = _medicalRecord.IllnessHistory.ToList<Illness>();
            Allergens = _medicalRecord.Allergens.ToList<Allergen>();
            PastAppointments = appointmentService.ReadFinishedAppointmentsForPatient(Patient).ToList<Appointment>();
        }
    }
}
