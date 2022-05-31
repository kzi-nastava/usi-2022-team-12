using HealthInstitution.Utility;

namespace HealthInstitution.Commands
{
    public class NavigateMedicineCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("DoctorMedicineManagment");
        }
    }
}
