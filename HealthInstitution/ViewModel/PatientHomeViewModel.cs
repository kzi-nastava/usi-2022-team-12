using HealthInstitution.Commands;
using HealthInstitution.Services.Intefaces;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class PatientHomeViewModel : ViewModelBase
    {
        public readonly IPatientService _patientService;

        public ICommand? AppointmentCreationWindowCommand { get; }

        public PatientHomeViewModel(IPatientService patientService)
        {
            _patientService = patientService;
            AppointmentCreationWindowCommand = new SimpleNavigateCommand<AppointmentCreationViewModel>();  
        }
    }
}
