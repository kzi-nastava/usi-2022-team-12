using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.SurveyManagement.Commands.ManagerCMD
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
