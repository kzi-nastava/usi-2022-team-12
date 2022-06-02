using HealthInstitution.ViewModel.doctor;

namespace HealthInstitution.Commands.doctor
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
