using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.UsersManagement;

namespace HealthInstitution.Core.Features.UsersManagement.Commands.PatientCMD
{
    public class SearchDoctorInfoCommand : CommandBase
    {
        DoctorSearchViewModel _viewModel;
        public SearchDoctorInfoCommand(DoctorSearchViewModel viewModel)
        {
            _viewModel = viewModel;

        }

        public override void Execute(object? parameter)
        {
            _viewModel.SearchDoctorInfo();
        }
    }
}
