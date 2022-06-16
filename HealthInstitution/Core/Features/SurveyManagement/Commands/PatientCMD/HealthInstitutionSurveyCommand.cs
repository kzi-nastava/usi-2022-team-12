using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.SurveyManagement.Commands.PatientCMD
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
