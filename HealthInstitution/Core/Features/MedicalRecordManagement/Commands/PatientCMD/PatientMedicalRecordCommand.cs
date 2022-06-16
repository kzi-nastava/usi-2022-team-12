using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.MedicalRecordManagement.Commands.PatientCMD
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
