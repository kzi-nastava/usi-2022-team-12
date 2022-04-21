using HealthInstitution.Ninject;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Commands
{
    /// <summary>
    /// Used for simple navigation that does not include any condition checks or other operations before changing view.
    /// Can be used for menu button navigation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class SimpleNavigateCommand<T> : CommandBase where T : ViewModelBase
    {
        public SimpleNavigateCommand()
        {

        }
        public override void Execute(object? parameter)
        {
            ViewModelBase viewModel = ServiceLocator.Get<T>();
            NavigationStore.CurrentViewModel = viewModel;
        }
    }
}
