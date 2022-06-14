using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Commands.PatientCMD
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
