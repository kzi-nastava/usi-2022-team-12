using HealthInstitution.Utility;

namespace HealthInstitution.Commands.manager
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
