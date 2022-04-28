using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
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
            if (SearchText == "")
                UpdatePage();
            else
                BlockedPatients = new ObservableCollection<Patient>(_patientService.FilterPatientsBySearchText(SearchText));
        }
    }
}
