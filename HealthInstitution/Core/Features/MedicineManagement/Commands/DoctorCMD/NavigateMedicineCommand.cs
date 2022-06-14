using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.MedicineManagement.Commands.DoctorCMD
{
    public class NavigateMedicineCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("DoctorMedicineManagment");
        }
    }
}
