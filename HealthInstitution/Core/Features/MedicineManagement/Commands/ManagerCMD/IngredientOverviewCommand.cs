using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.MedicineManagement.Commands.ManagerCMD
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
