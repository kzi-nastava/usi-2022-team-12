using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.RoomManagement.Commands.ManagerCMD
{
    public class CreateRoomCommand : CommandBase
    {

        public CreateRoomCommand()
        {

        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("CreateRoom");
        }
    }
}
