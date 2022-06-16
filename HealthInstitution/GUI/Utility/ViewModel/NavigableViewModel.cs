using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.GUI.Utility.ViewModel
{
    public class NavigableViewModel : ObservableEntity
    {
        private object _currentViewModel;
        public object CurrentViewModel { get => _currentViewModel; set => OnPropertyChanged(ref _currentViewModel, value); }

        public NavigableViewModel()
        {
        }

        public void SwitchCurrentViewModel(object nextViewModel)
        {
            CurrentViewModel = nextViewModel;
        }
    }
}
