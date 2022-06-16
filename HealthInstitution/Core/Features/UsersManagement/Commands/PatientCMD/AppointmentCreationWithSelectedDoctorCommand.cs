using System.ComponentModel;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.UsersManagement;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.UsersManagement.Commands.PatientCMD
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
