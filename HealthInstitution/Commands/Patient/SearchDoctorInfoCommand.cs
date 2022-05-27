using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
{
    public class SearchDoctorInfoCommand : CommandBase
    {
        DoctorSearchViewModel _viewModel;
        public SearchDoctorInfoCommand(DoctorSearchViewModel viewModel)
        {
            _viewModel = viewModel;

        }

        public override void Execute(object? parameter)
        {
            _viewModel.SearchDoctorInfo();
        }
    }
}
