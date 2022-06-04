using System.ComponentModel;
using HealthInstitution.Model;
using HealthInstitution.Model.medicine;
using HealthInstitution.ViewModel.doctor;

namespace HealthInstitution.Commands.doctor
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
            MedicineReview medicineReview = new MedicineReview { Comment = _viewModel.RevisionComment, Medicine = medicine, Doctor = _viewModel.Doctor};
            _viewModel.MedicineReviewService.Create(medicineReview);
            _viewModel.RevisionComment = "";
            _viewModel.SelectedMedicine = null;
            _viewModel.Ingredients = null;
            _viewModel.Medicines = _viewModel.MedicineService.GetPendingMedicine();
        }
    }
}
