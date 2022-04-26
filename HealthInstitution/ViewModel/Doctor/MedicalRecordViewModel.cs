using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class MedicalRecordViewModel : ViewModelBase
    {
        private readonly MedicalRecordService _medicalRecordService;

        private Patient _patient;

        private MedicalRecord _medicalRecord;
        public string PatientFullName => _patient.FullName;

        public string Height => _medicalRecord.Height.ToString();

        public string Age => CalculateAge(_patient.DateOfBirth).ToString(); 

        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Subtract(dateOfBirth).Days;
            age = age / 365;
            return age;            
        }

        private readonly ObservableCollection<Illness> _illnessHistoryData;
        public IEnumerable<Illness> IllnessHistoryData => _illnessHistoryData;

        private readonly ObservableCollection<Allergen> _allergens;
        public IEnumerable<Allergen > Allergens => _allergens;
        public string Weight =>_medicalRecord.Weight.ToString();
        public ICommand? BackCommand { get; }
        public MedicalRecordViewModel(MedicalRecordService medicalRecordService, Patient patient)
        {
            BackCommand = new NavigateScheduleCommand();
            _medicalRecordService = medicalRecordService;
            _patient = patient;
            _medicalRecord = medicalRecordService.GetMedicalRecordForPatient(patient);
            _illnessHistoryData = new ObservableCollection<Illness>();
            foreach(var illnes in _medicalRecord.IllnessHistory)
            {
                _illnessHistoryData.Add(illnes);
            }
            _allergens = new ObservableCollection<Allergen>();
            foreach(var allergen in _medicalRecord.Allergens)
            {
                _allergens.Add(allergen);
            }
        }
    }
}
