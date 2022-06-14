using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.Dialogs.Custom.Doctor;
using HealthInstitution.GUI.Utility.Dialog.Service;

namespace HealthInstitution.Commands.doctor
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
            foreach(var entry in _viewModel.Inventory)
            {
                _viewModel.EntryService.Update(entry);
            }
            _viewModel.CloseDialogWithResult((IDialogWindow)parameter, true);
        }
    }
}
