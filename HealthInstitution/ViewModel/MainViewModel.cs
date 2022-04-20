using HealthInstitution.Utility;

namespace HealthInstitution.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase? CurrentViewModel => NavigationStore.CurrentViewModel;

        public MainViewModel() {
            NavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        public void OnCurrentViewModelChanged() { 
            OnPropertyChanged(nameof(CurrentViewModel)); 
        }
    }
}
