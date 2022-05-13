using HealthInstitution.Dialogs.Custom;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class SecretaryReferralUsageViewModel : ObservableEntity
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

        private readonly IDialogService _dialogService;
        private readonly IPatientService _patientService;
        private readonly IReferralService _referralService;

        #endregion

        #region Commands

        public ICommand SearchCommand { get; private set; }

        public ICommand ShowReferrals { get; private set; }

        #endregion

        public SecretaryReferralUsageViewModel(IDialogService dialogService, IPatientService patientService,
            IReferralService referralService)
        {
            Patients = new ObservableCollection<Patient>(patientService.ReadAllValidPatients());
            _patientService = patientService;
            _referralService = referralService;
            _dialogService = dialogService;

            SearchCommand = new RelayCommand(() =>
            {
                Search();
            });

            ShowReferrals = new RelayCommand(() =>
            {
                if (_selectedPatient == null)
                {
                    MessageBox.Show("You did not select any patient");
                }
                else
                {
                    ReferralUsageViewModel refferalUsageViewModel = new ReferralUsageViewModel(_referralService);
                    _dialogService.OpenDialog(refferalUsageViewModel);
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
