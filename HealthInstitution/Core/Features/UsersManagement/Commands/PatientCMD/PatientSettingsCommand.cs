using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;

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
