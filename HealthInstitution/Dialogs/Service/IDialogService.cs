using System;

namespace HealthInstitution.Dialogs.Service
{
    public interface IDialogService
    {
        public void OpenDialog<T>(DialogViewModelBase<T> viewModel);

        public Tuple<R, bool?> OpenDialogWithReturnType<T, R>(DialogReturnViewModelBase<T, R> viewModel);
    }
}
