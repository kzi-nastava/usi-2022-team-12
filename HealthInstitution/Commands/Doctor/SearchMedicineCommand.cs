using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
{
    public class SearchMedicineCommand : CommandBase
    {
        private readonly PrescriptionViewModel _viewModel;
        public SearchMedicineCommand(PrescriptionViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _viewModel.UpdateData(_viewModel.SearchText);
        }
    }
}
