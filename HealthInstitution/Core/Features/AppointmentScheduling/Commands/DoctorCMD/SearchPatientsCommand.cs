using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.AppointmentScheduling;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Commands.DoctorCMD
{
    public class SearchPatientsCommand : CommandBase
    {
        private readonly IDoctorAppointmentViewModel _viewModel;
        public SearchPatientsCommand(IDoctorAppointmentViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _viewModel.UpdateData(_viewModel.SearchText);
        }
    }
}
