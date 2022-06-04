using HealthInstitution.Utility;

namespace HealthInstitution.Commands.patient.Navigation
{
    public class PatientSettingsCommand : CommandBase
    {
        public PatientSettingsCommand()
        {

        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("PatientSettings");
        }
    }
}
