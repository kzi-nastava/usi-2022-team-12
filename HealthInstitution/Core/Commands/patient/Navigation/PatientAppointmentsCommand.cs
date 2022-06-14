using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Commands.patient.Navigation
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
