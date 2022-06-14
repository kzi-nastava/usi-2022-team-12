using System.ComponentModel;
using System.Windows;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.GUI.Features.MedicalRecordManagement;
using HealthInstitution.GUI.Utility.Navigation;

namespace HealthInstitution.Core.Features.SurveyManagement.Commands.PatientCMD
{
    public class OpenDoctorSurveyCommand : CommandBase
    {
        private readonly PatientMedicalRecordViewModel? _viewModel;
        public OpenDoctorSurveyCommand(PatientMedicalRecordViewModel viewModel)
        {
            _viewModel = viewModel;
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
            return !(_viewModel.SelectedAppointment == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if (_viewModel.SelectedAppointment.IsRated == true) {
                MessageBox.Show("You already rated doctor for this appointment!");
                return;
            }
            GlobalStore.AddObject("SelectedAppointment", _viewModel.SelectedAppointment);
            EventBus.FireEvent("OpenDoctorSurvey");
        }
    }
}
