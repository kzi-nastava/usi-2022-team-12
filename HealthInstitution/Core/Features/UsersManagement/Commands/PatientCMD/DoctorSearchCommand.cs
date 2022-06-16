using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.UsersManagement.Commands.PatientCMD
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
