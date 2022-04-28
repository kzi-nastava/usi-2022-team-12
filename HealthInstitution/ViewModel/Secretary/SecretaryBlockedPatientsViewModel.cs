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

        public ICommand UnblockPatient { get; private set; }

        #endregion

        public SecretaryBlockedPatientsViewModel(IPatientService patientService)
        {
            BlockedPatients = new ObservableCollection<Patient>(patientService.ReadAllBlockedPatients());
            _patientService = patientService;

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
    }
}
