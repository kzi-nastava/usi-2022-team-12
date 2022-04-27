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

        private readonly IDialogService _dialogService;

        #endregion

        #region Commands

        public ICommand AddPatient { get; private set; }

        public ICommand UpdatePatient { get; private set; }

        public ICommand BlockPatient { get; private set; }

        #endregion

        public SecretaryPatientCRUDViewModel(IDialogService dialogService, IPatientService patientService)
        {
            Patients = new ObservableCollection<Patient>(patientService.ReadAllValidPatients());
            _patientService = patientService;
            _dialogService = dialogService;

            AddPatient = new RelayCommand(() =>
            {
                HandlePatientViewModel handlePatientViewModel = new HandlePatientViewModel(dialogService, patientService, this, Guid.Empty);
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
                    HandlePatientViewModel updatePatientViewModel = new HandlePatientViewModel(dialogService, patientService, this, _selectedPatient.Id);
                    _dialogService.OpenDialog(updatePatientViewModel);
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
                    UpdatePage();
                }
            });

        }

        public void UpdatePage()
        {
            Patients = new ObservableCollection<Patient>(_patientService.ReadAllValidPatients());

        }
    }
}
