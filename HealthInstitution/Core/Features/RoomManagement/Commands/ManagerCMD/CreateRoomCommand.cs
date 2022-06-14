using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;

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
