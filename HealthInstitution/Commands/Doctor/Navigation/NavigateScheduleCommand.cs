using HealthInstitution.Utility;

namespace HealthInstitution.Commands.doctor.Navigation
{
    public class NavigateScheduleCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("DoctorSchedule");
        }
    }
}
