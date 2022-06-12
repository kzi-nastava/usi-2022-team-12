using HealthInstitution.Utility;

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
