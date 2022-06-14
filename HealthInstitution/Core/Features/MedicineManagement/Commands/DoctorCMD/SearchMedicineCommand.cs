using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.Core.Utility.HelperClasses;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.ViewModel.doctor;

namespace HealthInstitution.Core.Features.MedicineManagement.Commands.DoctorCMD
{
    public class SearchMedicineCommand : CommandBase
    {
        private readonly ISearchMedicineViewModel _viewModel;
        private readonly IMedicineService _medicineService;
        private readonly Status _medicineStatus;

        public SearchMedicineCommand(ISearchMedicineViewModel viewModel, IMedicineService medicineService, Status medicineStatus)
        {
            _viewModel = viewModel;
            _medicineService = medicineService;
            _medicineStatus = medicineStatus;
        }
        public override void Execute(object? parameter)
        {
            _viewModel.Medicines = _medicineService.FilterMedicineBySearchText(_viewModel.SearchText, _medicineStatus);
        }
    }
}
