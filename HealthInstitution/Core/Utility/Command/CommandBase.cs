using System;
using System.Windows.Input;

namespace HealthInstitution.Core.Utility.Command
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? parameter);

        protected void OnCanExecuteChange()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        protected void OnCanExecuteChange(object sender, EventArgs e)
        {
            CanExecuteChanged?.Invoke(sender, e);
        }
    }
}
