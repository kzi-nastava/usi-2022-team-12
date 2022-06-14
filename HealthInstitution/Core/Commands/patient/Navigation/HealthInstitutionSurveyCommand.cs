using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Commands.patient.Navigation
{
    public class HealthInstitutionSurveyCommand : CommandBase
    {
        public HealthInstitutionSurveyCommand()
        {

        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("HealthInstitutionSurvey");
        }
    }
}
