using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.OperationsAndExaminations;

namespace HealthInstitution.Core.Features.OperationsAndExaminations.Commands.DoctorCMD
{
    public class SearchDoctorCommand : CommandBase
    {
        private readonly DoctorReferralCreationViewModel _viewModel;
        public SearchDoctorCommand(DoctorReferralCreationViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _viewModel.UpdateData(_viewModel.SearchText);
        }
    }
}
