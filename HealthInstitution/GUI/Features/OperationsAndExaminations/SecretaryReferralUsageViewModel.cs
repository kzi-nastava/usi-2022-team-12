using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.OperationsAndExaminations.Repository;
using HealthInstitution.Core.Features.OperationsAndExaminations.Services;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Service;
using HealthInstitution.Core.Ninject;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.Core.Utility.HelperClasses;
using HealthInstitution.GUI.Features.AppointmentScheduling.Dialog;
using HealthInstitution.GUI.Utility.Dialog.Service;

namespace HealthInstitution.GUI.Features.OperationsAndExaminations
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
        private readonly IReferralService _referralService;
        private readonly IPatientService _patientService;

        #endregion

        #region Commands

        public ICommand SearchCommand { get; private set; }

        public ICommand ShowReferrals { get; private set; }

        #endregion

        public SecretaryReferralUsageViewModel(IDialogService dialogService, IReferralService referralService,
            IPatientService patientService)
        {
            Patients = new ObservableCollection<Patient>(patientService.ReadAllValidPatients());
            _referralService = referralService;
            _patientService = patientService;
            _dialogService = dialogService;

            SearchCommand = new RelayCommand(Search);

            ShowReferrals = new RelayCommand(() =>
            {
                if (!_referralService.PatientHasValidReferral(_selectedPatient.Id))
                {
                    MessageBox.Show("Patient does not have any referral.");
                }
                else
                {
                    var referralUsageViewModel = new ReferralUsageViewModel(SelectedPatient.Id, _referralService);
                    _dialogService.OpenDialog(referralUsageViewModel);
                }
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
                Patients = new ObservableCollection<Patient>(
                    _patientService.FilterValidPatientsBySearchText(SearchText));

        }
    }
}
