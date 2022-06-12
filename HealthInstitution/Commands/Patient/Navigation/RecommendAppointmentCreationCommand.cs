using HealthInstitution.Utility;

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
