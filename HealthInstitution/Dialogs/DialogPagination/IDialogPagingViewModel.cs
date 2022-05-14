using HealthInstitution.ViewModel;
using System.Windows.Input;

namespace HealthInstitution.Dialogs.DialogPagination
{
    public interface IPagingViewModel
    {
        public int Rows { get; }
        public int Columns { get; }
        public PaginationViewModel PaginationViewModel { get; }

        public ICommand UpdatePageCommand { get; }
        public void UpdatePage(int pageNumber);
    }
}
