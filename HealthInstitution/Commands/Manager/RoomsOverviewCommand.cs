using HealthInstitution.Utility;

namespace HealthInstitution.Commands.manager
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


