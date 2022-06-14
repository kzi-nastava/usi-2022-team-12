using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Commands.manager
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
