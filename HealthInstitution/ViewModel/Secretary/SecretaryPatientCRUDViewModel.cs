using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HealthInstitution.ViewModel
{
    public class SecretaryPatientCRUDViewModel : ViewModelBase
    {
        public ObservableCollection<Model.Patient> Patients { get; set; }

        private readonly IPatientService _patientService;

        public SecretaryPatientCRUDViewModel(IPatientService patientService)
        {
            Patients = new ObservableCollection<Model.Patient>(patientService.ReadAll());
            _patientService = patientService;
        }
    }
}
