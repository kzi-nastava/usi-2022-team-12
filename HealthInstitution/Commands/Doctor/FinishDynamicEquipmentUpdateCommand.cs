using HealthInstitution.Dialogs.Custom.Doctor;
using HealthInstitution.Dialogs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
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
