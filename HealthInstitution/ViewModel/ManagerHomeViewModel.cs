using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Ninject;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class ManagerHomeViewModel : NavigableViewModel
    {
        public ICommand LogOutCommand { get; set; }

        public ICommand? RoomsOverviewCommand { get; set; }

        public ManagerHomeViewModel ()
        {
            RoomsOverviewCommand = new RoomsOverviewCommand();
            LogOutCommand = new LogOutCommand ();
            SwitchCurrentViewModel(ServiceLocator.Get<RoomsCRUDViewModel>());
            RegisterHandler();
        }

        private void RegisterHandler()
        {
            EventBus.RegisterHandler("RoomsOverview", () =>
            {
                RoomsCRUDViewModel Rcvm = ServiceLocator.Get<RoomsCRUDViewModel>();
                SwitchCurrentViewModel(Rcvm);
            });

            EventBus.RegisterHandler("RoomEquipment", () =>
            {
                RoomEquipmentViewModel Revm = ServiceLocator.Get<RoomEquipmentViewModel>();
                SwitchCurrentViewModel(Revm);
            });

            EventBus.RegisterHandler("CreateRoom", () =>
            {
                RoomCreateViewModel Rcvm = ServiceLocator.Get<RoomCreateViewModel>();
                SwitchCurrentViewModel(Rcvm);
            });

            EventBus.RegisterHandler("OpenUpdateRoom", () =>
            {   
                RoomUpdateViewModel Ruvm = ServiceLocator.Get<RoomUpdateViewModel>();
                SwitchCurrentViewModel(Ruvm);
            });
        }
    }
}
