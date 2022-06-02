using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthInstitution.Commands
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
            _viewModel.MedicineReviewService.Delete(GlobalStore.ReadObject<MedicineReview>("SelectedRejectedMedicine").Id);
            EventBus.FireEvent("MedicineOverview");

        }
    }
}