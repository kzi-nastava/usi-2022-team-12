﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.MedicalRecordManagement.Repository;
using HealthInstitution.Core.Features.MedicalRecordManagement.Service;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Service;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.Core.Utility.HelperClasses;
using HealthInstitution.GUI.Features.UsersManagement.Dialog;
using HealthInstitution.GUI.Utility.Dialog.Service;

namespace HealthInstitution.GUI.Features.UsersManagement
{
    public class SecretaryPatientCRUDViewModel : ObservableEntity
    {
        #region Properties

        private string _searchText;
        public string SearchText { get => _searchText; set => OnPropertyChanged(ref _searchText, value); }

        private ObservableCollection<Patient> _patients;
        public ObservableCollection<Patient> Patients
        {
            get { return _patients; }
            set { OnPropertyChanged(ref _patients, value); }
        }

        private Patient? _selectedPatient;
        public Patient? SelectedPatient
        {
            get { return _selectedPatient; }
            set { OnPropertyChanged(ref _selectedPatient, value); }
        }

        #endregion

        #region Services

        private readonly IUserService _userService;

        private readonly IPatientService _patientService;

        private readonly IMedicalRecordService _medicalRecordService;

        private readonly IDialogService _dialogService;

        private readonly ISchedulingService _schedulingService;

        #endregion

        #region Commands

        public ICommand SearchCommand { get; private set; }

        public ICommand AddPatient { get; private set; }

        public ICommand UpdatePatient { get; private set; }

        public ICommand DeletePatient { get; private set; }

        public ICommand BlockPatient { get; private set; }

        #endregion

        public SecretaryPatientCRUDViewModel(IDialogService dialogService, IUserService userService,
            IPatientService patientService, IMedicalRecordService medicalRecordService, ISchedulingService schedulingService)
        {
            Patients = new ObservableCollection<Patient>(patientService.ReadAllValidPatients());
            _userService = userService;
            _patientService = patientService;
            _patientService = patientService;
            _medicalRecordService = medicalRecordService;
            _dialogService = dialogService;
            _schedulingService = schedulingService;

            SearchCommand = new RelayCommand(Search);

            AddPatient = new RelayCommand(() =>
            {
                HandlePatientViewModel handlePatientViewModel = new HandlePatientViewModel(dialogService, userService, patientService, medicalRecordService, this, Guid.Empty);
                var isFinished = _dialogService.OpenDialog(handlePatientViewModel);
                if ((bool)isFinished)
                    MessageBox.Show("Patient added successfully.");
            });

            UpdatePatient = new RelayCommand(() =>
            {
                HandlePatientViewModel updatePatientViewModel = new HandlePatientViewModel(dialogService, userService, patientService, medicalRecordService, this, _selectedPatient.Id);
                var isFinished = _dialogService.OpenDialog(updatePatientViewModel);
                if ((bool)isFinished)
                    MessageBox.Show("Patient updated successfully.");
            }, () => SelectedPatient != null);

            DeletePatient = new RelayCommand(() =>
            {
                if (_schedulingService.PatientHasAnAppointment(_selectedPatient.Id))
                {
                    MessageBox.Show("Patient can not be deleted.");
                }
                else
                {
                    _patientService.Delete(_selectedPatient.Id);
                    MessageBox.Show("Patient deleted successfully.");
                    UpdatePage();
                }
            }, () => SelectedPatient != null);

            BlockPatient = new RelayCommand(() =>
            {
                _patientService.BlockPatient(_selectedPatient, BlockType.SECRETARY);
                MessageBox.Show("Patient blocked successfully.");
                UpdatePage();

            }, () => SelectedPatient != null);

        }

        public void UpdatePage()
        {
            Patients = new ObservableCollection<Patient>(_patientService.ReadAllValidPatients());
        }

        private void Search()
        {
            if (SearchText is "" or null)
                UpdatePage();
            else
                Patients = new ObservableCollection<Patient>(_patientService.FilterValidPatientsBySearchText(SearchText));
        }
    }
}
