using System;

namespace HealthInstitution.Dialogs.Service
{
    public class DialogService : IDialogService
    {
        public void OpenDialog<T>(DialogViewModelBase<T> viewModel)
        {
            IDialogWindow window = new DialogWindow
            {
                DataContext = viewModel
            };
            window.ShowDialog();
        }
    }
}
