using HealthInstitution.Dialogs.Custom;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.Model;
using HealthInstitution.Ninject;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
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

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set { OnPropertyChanged(ref _selectedPatient, value); }
        }

        #endregion

        #region Services

        private readonly IPatientService _patientService;

        private readonly IMedicalRecordService _medicalRecordService;

        private readonly IDialogService _dialogService;

        private readonly IAppointmentService _appointmentService;

        #endregion

        #region Commands

        public ICommand SearchCommand { get; private set; }

        public ICommand AddPatient { get; private set; }

        public ICommand UpdatePatient { get; private set; }

        public ICommand DeletePatient { get; private set; }

        public ICommand BlockPatient { get; private set; }

        #endregion

        public SecretaryPatientCRUDViewModel(IDialogService dialogService, IPatientService patientService, IMedicalRecordService medicalRecordService, IAppointmentService appointmentService)
        {
            Patients = new ObservableCollection<Patient>(patientService.ReadAllValidPatients());
            _patientService = patientService;
            _medicalRecordService = medicalRecordService;
            _dialogService = dialogService;
            _appointmentService = appointmentService;

            SearchCommand = new RelayCommand(() =>
            {
                Search();
            });

            AddPatient = new RelayCommand(() =>
            {
                HandlePatientViewModel handlePatientViewModel = new HandlePatientViewModel(dialogService, patientService, medicalRecordService, this, Guid.Empty);
                _dialogService.OpenDialog(handlePatientViewModel);
            });

            UpdatePatient = new RelayCommand(() =>
            {
                if (_selectedPatient == null)
                {
                    MessageBox.Show("You did not select any patient to update.");
                }
                else
                {
                    HandlePatientViewModel updatePatientViewModel = new HandlePatientViewModel(dialogService, patientService, medicalRecordService, this, _selectedPatient.Id);
                    _dialogService.OpenDialog(updatePatientViewModel);
                    MessageBox.Show("Patient updated succesfully.");
                }
            });

            DeletePatient = new RelayCommand(() =>
            {
                if (_selectedPatient == null)
                {
                    MessageBox.Show("You did not select any patient to update.");
                }
                else if (_appointmentService.PatientHasAnAppointment(_selectedPatient.Id))
                {
                    MessageBox.Show("Patient can not be deleted.");
                }
                else
                {
                    _patientService.Delete(_selectedPatient.Id);
                    MessageBox.Show("Patient deleted succesfully.");
                    UpdatePage();
                }
            });

            BlockPatient = new RelayCommand(() =>
            {
                if (_selectedPatient == null)
                {
                    MessageBox.Show("You did not select any patient to delete.");
                }
                else
                {
                    _patientService.BlockPatient(_selectedPatient);
                    MessageBox.Show("Patient blocked succesfully.");
                    UpdatePage();
                }
            });

        }

        public void UpdatePage()
        {
            Patients = new ObservableCollection<Patient>(_patientService.ReadAllValidPatients());
        }

        private void Search()
        {
            if (SearchText == "" || SearchText == null)
                UpdatePage();
            else
                Patients = new ObservableCollection<Patient>(_patientService.FilterValidPatientsBySearchText(SearchText));
        }
    }
}
