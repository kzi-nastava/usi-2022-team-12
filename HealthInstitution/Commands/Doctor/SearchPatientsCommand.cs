using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
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
