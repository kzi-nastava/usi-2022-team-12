using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
{
    public class SearchByAnamnesisCommand : CommandBase
    {
        PatientMedicalRecordViewModel _viewModel;
        public SearchByAnamnesisCommand(PatientMedicalRecordViewModel viewModel) {
            _viewModel = viewModel;

        }
    }
}
