using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Service;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.GUI.Features.UsersManagement
{
    public class SecretaryBlockedPatientsViewModel : ObservableEntity
    {
        #region Properties

        private string _searchText;
        public string SearchText { get => _searchText; set => OnPropertyChanged(ref _searchText, value); }

        private ObservableCollection<Patient> _blockedPatients;
        public ObservableCollection<Patient> BlockedPatients
        {
            get { return _blockedPatients; }
            set { OnPropertyChanged(ref _blockedPatients, value); }
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

        #endregion

        #region Commands

        public ICommand SearchCommand { get; private set; }

        public ICommand UnblockPatient { get; private set; }

        #endregion

        public SecretaryBlockedPatientsViewModel(IPatientService patientService)
        {
            BlockedPatients = new ObservableCollection<Patient>(patientService.ReadAllBlockedPatients());
            _patientService = patientService;

            SearchCommand = new RelayCommand(() =>
            {
                Search();
            });

            UnblockPatient = new RelayCommand(() =>
            {
                if (_selectedPatient == null)
                {
                    MessageBox.Show("You did not select any patient to unblock.");
                }
                else
                {
                    _patientService.UnblockPatient(_selectedPatient);
                    MessageBox.Show("Patient unblocked succesfully.");
                    UpdatePage();
                }
            });

        }

        private void UpdatePage()
        {
            BlockedPatients = new ObservableCollection<Patient>(_patientService.ReadAllBlockedPatients());
        }

        private void Search()
        {
            if (SearchText == "" || SearchText == null)
                UpdatePage();
            else
                BlockedPatients = new ObservableCollection<Patient>(_patientService.FilterBlockedPatientsBySearchText(SearchText));
        }
    }
}
