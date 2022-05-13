using HealthInstitution.Commands;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.Pagination;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System.Windows.Input;

namespace HealthInstitution.Dialogs.DialogPagination
{
    public abstract class DialogPagingViewModelBase<T> : DialogViewModelBase<T>, IPagingViewModel
    {
        public int Rows { get; protected set; } = 2;
        public int Columns { get; protected set; } = 2;
        public int Size => Rows * Columns;

        public ICommand UpdatePageCommand { get; protected set; }

        public PaginationViewModel PaginationViewModel { get; protected set; }

        public DialogPagingViewModelBase(string title, string message, int windowWidth, int windowHeight) : base(title, message, windowWidth, windowHeight)
        {
            UpdatePageCommand = new UpdatePageCommand(this);
        }

        public abstract void UpdatePage(int pageNumber);

        protected void OnPageFetched<TEntity>(Page<TEntity> page) where TEntity : ObservableEntity
        {
            PaginationViewModel = new PaginationViewModel(page.PageNumber, page.TotalElements, page.Size, page.PageCount);
            OnPropertyChanged(nameof(PaginationViewModel));
        }
    }
}
