using System.ComponentModel;
using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.ViewModel.patient;

namespace HealthInstitution.Commands.patient.Navigation
{
    public class AppointmentCreationWithSelectedDoctorCommand : CommandBase
    {
        private readonly DoctorSearchViewModel? _viewModel;
        public AppointmentCreationWithSelectedDoctorCommand(DoctorSearchViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.SelectedDoctorInfo))
            {
                OnCanExecuteChange();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_viewModel.SelectedDoctorInfo == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            GlobalStore.AddObject("SelectedDoctor", _viewModel.SelectedDoctorInfo.Doctor);
            EventBus.FireEvent("AppointmentCreationWithSelectedDoctor");
        }
    }


}
