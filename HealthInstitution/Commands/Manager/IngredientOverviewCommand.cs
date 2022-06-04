using HealthInstitution.Utility;

namespace HealthInstitution.Commands.manager
{
    public class IngredientOverviewCommand : CommandBase
    {
        public IngredientOverviewCommand()
        {
        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("IngredientOverview");
        }
    }
}
