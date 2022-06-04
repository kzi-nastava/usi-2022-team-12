using HealthInstitution.Utility;

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
