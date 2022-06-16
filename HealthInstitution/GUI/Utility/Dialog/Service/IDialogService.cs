using System;

namespace HealthInstitution.GUI.Utility.Dialog.Service
{
    public interface IDialogService
    {
        public bool? OpenDialog<T>(DialogViewModelBase<T> viewModel);

        public Tuple<R, bool?> OpenDialogWithReturnType<T, R>(DialogReturnViewModelBase<T, R> viewModel);
    }
}
