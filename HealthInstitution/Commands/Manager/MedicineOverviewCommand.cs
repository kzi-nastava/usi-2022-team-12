using HealthInstitution.Utility;

namespace HealthInstitution.Commands.manager
{
    public class MedicineOverviewCommand : CommandBase
    {
        public MedicineOverviewCommand()
        {
        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("MedicineOverview");
        }
    }
}
