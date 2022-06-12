using HealthInstitution.Utility;

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
