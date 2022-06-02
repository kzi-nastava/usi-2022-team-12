using HealthInstitution.Utility;

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
