using System.ComponentModel;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.Core.Utility.HelperClasses;
using HealthInstitution.GUI.Features.MedicineManagement;

namespace HealthInstitution.Core.Features.MedicineManagement.Commands.DoctorCMD
{
    public class MarkForRevisionCommand : CommandBase
    {
        private readonly DoctorMedicineManagmentViewModel _viewModel;

        public MarkForRevisionCommand(DoctorMedicineManagmentViewModel viewModel)
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
            medicine.Status = Status.Rejected;
            _viewModel.MedicineService.Update(medicine);
            MedicineReview medicineReview = new MedicineReview { Comment = _viewModel.RevisionComment, Medicine = medicine, Doctor = _viewModel.Doctor };
            _viewModel.MedicineReviewRepository.Create(medicineReview);
            _viewModel.RevisionComment = "";
            _viewModel.SelectedMedicine = null;
            _viewModel.Ingredients = null;
            _viewModel.Medicines = _viewModel.MedicineService.GetPendingMedicine();
        }
    }
}
