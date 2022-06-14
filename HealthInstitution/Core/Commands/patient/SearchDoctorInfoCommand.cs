using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.ViewModel.patient;

namespace HealthInstitution.Commands.patient
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
