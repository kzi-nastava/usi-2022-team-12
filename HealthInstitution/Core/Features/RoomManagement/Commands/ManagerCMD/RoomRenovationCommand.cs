using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD
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
