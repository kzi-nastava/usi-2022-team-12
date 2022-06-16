using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthInstitution.Core.Features.AppointmentScheduling.Commands.DoctorCMD;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Features.MedicalRecordManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.MedicalRecordManagement
{
    public class MedicalRecordViewModel : ViewModelBase
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        private readonly ISchedulingService _schedulingService;

        private readonly Patient _patient;

        private readonly MedicalRecord _medicalRecord;
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
        public IEnumerable<Allergen> Allergens => _allergens;

        private readonly ObservableCollection<Appointment> _appointments;
        public IEnumerable<Appointment> Appointments => _appointments;
        public string Weight => _medicalRecord.Weight.ToString();
        public ICommand? BackCommand { get; }
        public MedicalRecordViewModel(IMedicalRecordRepository medicalRecordRepository, ISchedulingService schedulingService, Patient patient)
        {
            BackCommand = new NavigateScheduleCommand();
            _schedulingService = schedulingService;
            _medicalRecordRepository = medicalRecordRepository;
            _patient = patient;
            _medicalRecord = _medicalRecordRepository.GetMedicalRecordForPatient(patient);
            _illnessHistoryData = new ObservableCollection<Illness>();
            foreach (var illnes in _medicalRecord.IllnessHistory)
            {
                _illnessHistoryData.Add(illnes);
            }
            _allergens = new ObservableCollection<Allergen>();
            foreach (var allergen in _medicalRecord.Allergens)
            {
                _allergens.Add(allergen);
            }
            _appointments = new ObservableCollection<Appointment>();
            IEnumerable<Appointment> apps = _schedulingService.ReadFinishedAppointmentsForPatient(patient);
            foreach (var appointment in apps)
            {
                _appointments.Add(appointment);
            }
        }
    }
}
