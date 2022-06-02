using HealthInstitution.Utility;
using System.ComponentModel;

namespace HealthInstitution.Commands
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
