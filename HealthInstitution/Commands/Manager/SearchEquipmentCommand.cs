using HealthInstitution.Model;
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
    public class SearchEquipmentCommand : CommandBase
    {

        private readonly EquipmentOverviewViewModel? _viewModel;
        public SearchEquipmentCommand(EquipmentOverviewViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.loadTable();
        }
    }
}