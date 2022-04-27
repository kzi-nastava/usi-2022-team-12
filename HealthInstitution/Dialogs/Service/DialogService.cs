using System;

namespace HealthInstitution.Dialogs.Service
{
    public class DialogService : IDialogService
    {
        public T OpenDialog<V, T>(DialogViewModelBase<V, T> viewModel)
        {
            IDialogWindow window = new DialogWindow
            {
                DataContext = viewModel
            };
            window.ShowDialog();

            return viewModel.DialogResult;
        }
    }
}
