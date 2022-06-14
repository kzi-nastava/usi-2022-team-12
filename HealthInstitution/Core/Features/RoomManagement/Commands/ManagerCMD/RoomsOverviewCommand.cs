using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;

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


