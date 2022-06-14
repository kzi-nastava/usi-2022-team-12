using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.SurveyManagement.Commands.ManagerCMD
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
