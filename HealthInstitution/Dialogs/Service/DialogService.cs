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

        public Tuple<R, bool?> OpenDialogWithReturnType<T, R>(DialogReturnViewModelBase<T, R> viewModel)
        {
            IDialogWindow window = new DialogWindow
            {
                DataContext = viewModel
            };
            window.ShowDialog();

            return Tuple.Create(viewModel.DialogResult, window.DialogResult);
        }
    }
}
