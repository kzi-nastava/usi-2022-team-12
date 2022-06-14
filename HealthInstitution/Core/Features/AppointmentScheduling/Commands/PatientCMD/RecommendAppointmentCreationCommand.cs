using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Commands.patient.Navigation
{
    public class RecommendAppointmentCreationCommand : CommandBase
    {
        public RecommendAppointmentCreationCommand() { }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("RecommendAppointmentCreation");
        }
    }
}
