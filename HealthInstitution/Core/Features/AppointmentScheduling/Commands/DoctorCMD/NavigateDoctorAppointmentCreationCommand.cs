using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.AppointmentScheduling;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Commands.DoctorCMD
{
    public class NavigateDoctorAppointmentCreationCommand : CommandBase
    {
        private DoctorScheduleViewModel _viewModel;
        public NavigateDoctorAppointmentCreationCommand(DoctorScheduleViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("CreateAppointment");
        }
    }
}
