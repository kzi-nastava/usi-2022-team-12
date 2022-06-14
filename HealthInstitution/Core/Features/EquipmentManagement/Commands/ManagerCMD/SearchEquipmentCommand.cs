using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.EquipmentManagement;

namespace HealthInstitution.Core.Features.EquipmentManagement.Commands.ManagerCMD
{
    public class SearchEquipmentCommand : CommandBase
    {

        private readonly EquipmentOverviewViewModel? _viewModel;
        public SearchEquipmentCommand(EquipmentOverviewViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.loadTable();
        }
    }
}