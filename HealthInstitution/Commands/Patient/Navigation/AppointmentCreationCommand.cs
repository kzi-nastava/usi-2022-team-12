using HealthInstitution.Utility;

namespace HealthInstitution.Commands.patient.Navigation
{
    public class AppointmentCreationCommand : CommandBase
    {
        public AppointmentCreationCommand()
        {

        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("AppointmentCreation");
        }
    }
}
