using HealthInstitution.ViewModel.patient;

namespace HealthInstitution.Commands.patient
{
    public class SearchByAnamnesisCommand : CommandBase
    {
        PatientMedicalRecordViewModel _viewModel;
        public SearchByAnamnesisCommand(PatientMedicalRecordViewModel viewModel) {
            _viewModel = viewModel;

        }

        public override void Execute(object? parameter)
        {
            _viewModel.SearchByAnamnesis();
        }
    }
}
