namespace HealthInstitution.Dialogs.Service
{
    public interface IDialogService
    {
        public void OpenDialog<T>(DialogViewModelBase<T> viewModel);
    }
}
