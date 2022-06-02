using HealthInstitution.Utility;

namespace HealthInstitution.Commands.doctor.Navigation
{
    public class NavigatePrescriptionCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("CreatePrescription");
        }
    }
}
