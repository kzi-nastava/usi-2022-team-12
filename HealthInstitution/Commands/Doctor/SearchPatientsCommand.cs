using HealthInstitution.ViewModel.doctor;

namespace HealthInstitution.Commands.doctor
{
    public class SearchPatientsCommand : CommandBase
    {
        private readonly IDoctorAppointmentViewModel _viewModel;
        public SearchPatientsCommand(IDoctorAppointmentViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _viewModel.UpdateData(_viewModel.SearchText);
        }
    }
}
