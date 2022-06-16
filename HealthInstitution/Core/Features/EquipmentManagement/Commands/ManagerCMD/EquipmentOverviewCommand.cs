using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.EquipmentManagement.Commands.ManagerCMD
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


