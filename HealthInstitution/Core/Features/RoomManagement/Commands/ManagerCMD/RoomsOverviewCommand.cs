using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD
{
    public class RoomsOverviewCommand : CommandBase
    {
        public RoomsOverviewCommand()
        {
        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("RoomsOverview");
        }
    }
}


