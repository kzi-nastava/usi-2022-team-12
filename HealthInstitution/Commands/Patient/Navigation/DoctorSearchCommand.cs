using HealthInstitution.Utility;

namespace HealthInstitution.Commands.patient.Navigation
{
    public class DoctorSearchCommand : CommandBase
    {
        public DoctorSearchCommand() { 
        
        }

        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("DoctorSearch");
        }
    }
}
