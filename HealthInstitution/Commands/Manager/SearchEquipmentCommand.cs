using HealthInstitution.ViewModel.manager;

namespace HealthInstitution.Commands.manager
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