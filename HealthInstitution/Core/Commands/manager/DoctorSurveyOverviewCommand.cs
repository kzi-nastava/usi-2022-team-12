using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Commands.manager
{
    public class DoctorSurveyOverviewCommand : CommandBase
    {
        public DoctorSurveyOverviewCommand()
        {
        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("DoctorSurveyOverview");
        }
    }
}
