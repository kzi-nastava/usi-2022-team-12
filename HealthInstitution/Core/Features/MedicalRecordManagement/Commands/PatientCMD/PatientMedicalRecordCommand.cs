using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Commands.patient.Navigation
{
    public class PatientMedicalRecordCommand : CommandBase
    {
        public PatientMedicalRecordCommand()
        {

        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("PatientMedicalRecord");
        }
    }
}
