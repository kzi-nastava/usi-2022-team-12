using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.OperationsAndExaminations.Commands.DoctorCMD
{
    public class NavigatePrescriptionCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("CreatePrescription");
        }
    }
}
