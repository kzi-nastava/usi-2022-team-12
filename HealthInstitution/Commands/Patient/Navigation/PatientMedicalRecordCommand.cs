using HealthInstitution.Utility;

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
