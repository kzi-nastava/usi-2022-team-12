using HealthInstitution.Dialogs.Service;
using System;

namespace HealthInstitution.GUI.Utility.Dialog.Service
{
    public class DialogService : IDialogService
    {
        public bool? OpenDialog<T>(DialogViewModelBase<T> viewModel)
        {
            IDialogWindow window = new DialogWindow
            {
                DataContext = viewModel
            };
            window.ShowDialog();
            return window.DialogResult;
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
