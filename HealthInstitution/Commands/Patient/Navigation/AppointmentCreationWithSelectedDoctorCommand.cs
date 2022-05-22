using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
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
