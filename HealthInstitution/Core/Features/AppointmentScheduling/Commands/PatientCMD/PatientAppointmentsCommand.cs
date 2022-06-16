using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Commands.PatientCMD
{
    public class PatientAppointmentsCommand : CommandBase
    {
        public PatientAppointmentsCommand() { 
        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("PatientAppointments");
        }
    }
}
