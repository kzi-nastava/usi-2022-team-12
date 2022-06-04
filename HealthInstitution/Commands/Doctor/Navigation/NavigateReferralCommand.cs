using HealthInstitution.Utility;

namespace HealthInstitution.Commands.doctor.Navigation
{
    public class NavigateReferralCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("CreateRefferal");
        }
    }
}
