using HealthInstitution.Utility;

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
