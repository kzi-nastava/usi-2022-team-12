using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Commands.manager
{
    public class RoomRenovationCommand : CommandBase
    {
        public RoomRenovationCommand()
        {
        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("RoomRenovation");
        }
    }
}
