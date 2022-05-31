using HealthInstitution.Model;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
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
            medicine.Status = Status.Revision;
            _viewModel.MedicineService.Update(medicine);
            MedicineReview medicineReview = new MedicineReview { Comment = _viewModel.RevisionComment, Medicine = medicine, Doctor = _viewModel.Doctor};
            _viewModel.MedicineReviewService.Create(medicineReview);
            _viewModel.RevisionComment = "";
            _viewModel.Medicines = _viewModel.MedicineService.GetPendingMedicine();
        }
    }
}
