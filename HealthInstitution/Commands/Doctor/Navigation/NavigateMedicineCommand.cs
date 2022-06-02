using HealthInstitution.Utility;

namespace HealthInstitution.Commands.doctor.Navigation
{
    public class NavigateMedicineCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("DoctorMedicineManagment");
        }
    }
}
