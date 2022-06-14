using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;

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
