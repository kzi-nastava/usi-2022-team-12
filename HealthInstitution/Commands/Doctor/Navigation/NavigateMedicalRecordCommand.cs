using System.ComponentModel;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using HealthInstitution.ViewModel.doctor;

namespace HealthInstitution.Commands.doctor.Navigation
{
    internal class NavigateMedicalRecordCommand : CommandBase
    {
        private DoctorScheduleViewModel _viewModel;
        public NavigateMedicalRecordCommand(ViewModelBase viewModel)
        {
            _viewModel = (DoctorScheduleViewModel)viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SelectedAppointment))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedAppointment is not null && base.CanExecute(parameter);
        }
        public override void Execute(object? parameter)
        {
            GlobalStore.AddObject("SelectedPatient", _viewModel.SelectedAppointment.Patient);
            EventBus.FireEvent("MedicalRecord");
        }
    }
}
