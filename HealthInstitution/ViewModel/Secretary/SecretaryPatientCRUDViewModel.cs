using HealthInstitution.Dialogs.Custom;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.Model;
using HealthInstitution.Ninject;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class SecretaryPatientCRUDViewModel : ObservableEntity
    {
        private ObservableCollection<Patient> _patients;
        public ObservableCollection<Patient> Patients
        {
            get { return _patients; }
            set { OnPropertyChanged(ref _patients, value); }
        }

        private readonly IPatientService _patientService;

        private readonly IDialogService _dialogService;

        public ICommand AddPatient { get; set; } 

        public SecretaryPatientCRUDViewModel(IDialogService dialogService, IPatientService patientService)
        {
            Patients = new ObservableCollection<Patient>(patientService.ReadAllValidPatients());
            _patientService = patientService;
            _dialogService = dialogService;

            AddPatientViewModel addPatientViewModel = new AddPatientViewModel(dialogService, patientService, this);

            AddPatient = new RelayCommand(() =>
            _dialogService.OpenDialog(addPatientViewModel));
        }

        public void UpdatePage()
        {
            Patients = new ObservableCollection<Patient>(_patientService.ReadAllValidPatients());

        }
    }
}
