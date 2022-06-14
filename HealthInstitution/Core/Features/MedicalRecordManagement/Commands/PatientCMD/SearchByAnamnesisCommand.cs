using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.MedicalRecordManagement;

namespace HealthInstitution.Core.Features.MedicalRecordManagement.Commands.PatientCMD
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
