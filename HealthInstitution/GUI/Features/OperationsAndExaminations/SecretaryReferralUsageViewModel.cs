using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Dialogs.Custom;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.Model;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.Utility;

namespace HealthInstitution.ViewModel.secretary
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
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        #endregion

        #region Commands

        public ICommand SearchCommand { get; private set; }

        public ICommand ShowReferrals { get; private set; }

        #endregion

        public SecretaryReferralUsageViewModel(IDialogService dialogService, IReferralService referralService,
            IAppointmentService appointmentService, IDoctorService doctorService, IPatientService patientService)
        {
            Patients = new ObservableCollection<Patient>(patientService.ReadAllValidPatients());
            _referralService = referralService;
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _patientService = patientService;
            _dialogService = dialogService;

            SearchCommand = new RelayCommand(Search);

            ShowReferrals = new RelayCommand(() =>
            {
                if (_selectedPatient == null)
                {
                    MessageBox.Show("You did not select any patient");
                }
                else if (!_referralService.PatientHasValidReferral(_selectedPatient.Id))
                {
                    MessageBox.Show("Patient does not have any referral.");
                }
                else
                {
                    ReferralUsageViewModel refferalUsageViewModel = new ReferralUsageViewModel(SelectedPatient.Id, _referralService,
                        _appointmentService, _doctorService, _patientService);
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
            if (SearchText is "" or null)
                UpdatePage();
            else
                Patients = new ObservableCollection<Patient>(
                    _patientService.FilterValidPatientsBySearchText(SearchText));

        }
    }
}
