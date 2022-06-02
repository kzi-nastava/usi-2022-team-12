using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
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
        private readonly ISearchMedicineViewModel _viewModel;
        private readonly IMedicineService _medicineService;
        private readonly Status _medicineStatus;

        public SearchMedicineCommand(ISearchMedicineViewModel viewModel, IMedicineService medicineService, Status medicineStatus)
        {
            _viewModel = viewModel;
            this._medicineService = medicineService;
            this._medicineStatus = medicineStatus;
        }
        public override void Execute(object? parameter)
        {
            _viewModel.Medicines = _medicineService.FilterMedicineBySearchText(_viewModel.SearchText, _medicineStatus);
        }
    }
}
