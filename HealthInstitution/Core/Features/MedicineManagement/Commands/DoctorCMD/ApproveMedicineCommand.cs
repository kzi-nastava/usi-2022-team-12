using System.ComponentModel;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.Core.Utility.HelperClasses;
using HealthInstitution.ViewModel.doctor;

namespace HealthInstitution.Core.Features.MedicineManagement.Commands.DoctorCMD
{
    public class ApproveMedicineCommand : CommandBase
    {
        private readonly DoctorMedicineManagmentViewModel _viewModel;

        public ApproveMedicineCommand(DoctorMedicineManagmentViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SelectedMedicine))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedMedicine is not null && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            Medicine medicine = _viewModel.SelectedMedicine;
            medicine.Status = Status.Approved;
            _viewModel.MedicineService.Update(medicine);
            _viewModel.SelectedMedicine = null;
            _viewModel.Ingredients = null;
            _viewModel.Medicines = _viewModel.MedicineService.GetPendingMedicine();
        }

    }
}
