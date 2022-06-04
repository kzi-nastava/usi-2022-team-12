using HealthInstitution.Utility;

namespace HealthInstitution.Commands.manager
{
    public class EquipmentOverviewCommand : CommandBase
    {
        public EquipmentOverviewCommand()
        {
        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("EquipmentOverview");
        }
    }
}


