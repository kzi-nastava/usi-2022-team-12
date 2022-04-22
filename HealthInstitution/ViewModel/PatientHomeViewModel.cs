using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class PatientHomeViewModel : ViewModelBase
    {
        public readonly IPatientService<Patient> _patientService;

        public ICommand? AppointmentCreationWindowCommand { get; }

        public PatientHomeViewModel(IPatientService<Patient> patientService)
        {
            _patientService = patientService;
            AppointmentCreationWindowCommand = new SimpleNavigateCommand<AppointmentCreationViewModel>();  
        }
    }
}
