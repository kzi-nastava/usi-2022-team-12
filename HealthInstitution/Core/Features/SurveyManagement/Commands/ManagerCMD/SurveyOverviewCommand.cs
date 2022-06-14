using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Commands.manager
{
    public class SurveyOverviewCommand : CommandBase
    {
        public SurveyOverviewCommand()
        {
        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("SurveyOverview");
        }
    }
}
