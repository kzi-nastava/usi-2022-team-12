using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;

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
