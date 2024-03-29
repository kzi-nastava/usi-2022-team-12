﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthInstitution.Core.Features.AppointmentScheduling.Commands.DoctorCMD;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Service;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.AppointmentScheduling
{
    public class DoctorAppointmentUpdateViewModel : ViewModelBase, IDoctorAppointmentViewModel
    {
        private IPatientRepository _patientRepository;

        private ISchedulingService _schedulingService;

        private IPatientService _patientService;
        public ISchedulingService SchedulingService => _schedulingService;
        public IPatientService PatientService => _patientService;

        private Appointment _appointment;
        public Appointment Appointment => _appointment;

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
        public ICommand CancelAppointmentUpdateCommand { get; }
        public ICommand UpdateAppointmentCommand { get; }
        public ICommand SearchPatientCommand { get; }
        public DoctorAppointmentUpdateViewModel(ISchedulingService schedulingService, IPatientRepository patientRepository, IPatientService patientService, Appointment selectedAppointment)
        {
            _appointment = selectedAppointment;
            _date = selectedAppointment.StartDate;
            _time = selectedAppointment.StartDate;
            _selectedPatient = selectedAppointment.Patient;
            _schedulingService = schedulingService;
            _patientRepository = patientRepository;
            _patientService = patientService;
            _patientService = patientService;
            UpdateData(null);
            CancelAppointmentUpdateCommand = new NavigateScheduleCommand();
            UpdateAppointmentCommand = new DoctorUpdateAppointmentCommand(this);
            SearchPatientCommand = new SearchPatientsCommand(this);
        }
        public void UpdateData(string prefix)
        {
            _patients = new ObservableCollection<Patient>();
            IEnumerable<Patient> patients = null;
            if (string.IsNullOrEmpty(prefix))
            {
                patients = _patientRepository.ReadAll();
            }
            else
            {
                patients = _patientService.FilterPatientsBySearchText(prefix);
            }
            foreach (var patient in patients)
            {
                _patients.Add(patient);
            }
            OnPropertyChanged(nameof(Patients));
        }

    }
}
