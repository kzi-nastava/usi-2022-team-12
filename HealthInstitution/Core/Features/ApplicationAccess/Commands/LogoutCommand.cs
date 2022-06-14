using HealthInstitution.Core.Utility.BaseCommand;
using HealthInstitution.Core.Utility.Checker;
using HealthInstitution.GUI.Utility.Navigation;
using System.ComponentModel;

namespace HealthInstitution.Core.Features.ApplicationAccess.Commands
{
    public class LogOutCommand : CommandBase
    {
        public LogOutCommand()
        {
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChange();
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            NotificationsChecker.StopTimer();
            EventBus.FireEvent("BackToLogin");
        }
    }
}
