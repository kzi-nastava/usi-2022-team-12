using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.EquipmentManagement.Commands.ManagerCMD
{
    public class ArrangeEquipmentCommand : CommandBase
    {
        public ArrangeEquipmentCommand()
        {
        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("ArrangeEquipment");
        }
    }
}
