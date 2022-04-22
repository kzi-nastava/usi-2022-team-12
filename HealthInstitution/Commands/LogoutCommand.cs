using HealthInstitution.Ninject;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.Commands
{
    internal class LogoutCommand : CommandBase
    {
        public LogoutCommand()
        {

        }

        public override void Execute(object? parameter)
        {
            GlobalStore.AddObject("LoggedUser", null);
            LoginViewModel lvm = ServiceLocator.Get<LoginViewModel>();
            NavigationStore.CurrentViewModel = lvm;
        }
    }
}
