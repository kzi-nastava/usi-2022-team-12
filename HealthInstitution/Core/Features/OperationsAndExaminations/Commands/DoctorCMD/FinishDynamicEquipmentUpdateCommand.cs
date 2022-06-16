using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.EquipmentManagement;
using HealthInstitution.GUI.Utility.Dialog.Service;

namespace HealthInstitution.Core.Features.OperationsAndExaminations.Commands.DoctorCMD
{
    public class FinishDynamicEquipmentUpdateCommand : CommandBase
    {
        private readonly DynamicEquipmentUpdateViewModel _viewModel;

        public FinishDynamicEquipmentUpdateCommand(DynamicEquipmentUpdateViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            foreach (var entry in _viewModel.Inventory)
            {
                _viewModel.EntryRepository.Update(entry);
            }
            _viewModel.CloseDialogWithResult((IDialogWindow)parameter, true);
        }
    }
}
