using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthInstitution.Commands.doctor.Navigation;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.GUI.Utility.ViewModel;
using HealthInstitution.Model.appointment;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.ViewModel.doctor
{
    public class MedicalRecordViewModel : ViewModelBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        private readonly IAppointmentService _appointmentService;

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
        public MedicalRecordViewModel(IMedicalRecordService medicalRecordService, IAppointmentService appointmentService, Patient patient)
        {
            BackCommand = new NavigateScheduleCommand();
            _appointmentService = appointmentService;
            _medicalRecordService = medicalRecordService;
            _patient = patient;
            _medicalRecord = medicalRecordService.GetMedicalRecordForPatient(patient);
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
            IEnumerable<Appointment> apps = appointmentService.ReadFinishedAppointmentsForPatient(patient);
            foreach (var appointment in apps)
            {
                _appointments.Add(appointment);
            }
        }
    }
}
