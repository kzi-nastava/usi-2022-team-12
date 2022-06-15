using System.ComponentModel;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.MedicineManagement;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.MedicineManagement.Commands.ManagerCMD
{
    public class RemoveRejectedCommand : CommandBase
    {
        private readonly MedicineOverviewViewModel? _viewModel;

        public RemoveRejectedCommand(MedicineOverviewViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SelectedRejectedMedicine))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedRejectedMedicine != null;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.MedicineReviewRepository.Delete(GlobalStore.ReadObject<MedicineReview>("SelectedRejectedMedicine").Id);
            EventBus.FireEvent("MedicineOverview");

        }
    }
}