using HealthInstitution.Commands;
using HealthInstitution.Model;
using HealthInstitution.Ninject;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class ManagerHomeViewModel : NavigableViewModel
    {
        public ICommand LogOutCommand { get; set; }

        public ICommand? RoomsOverviewCommand { get; set; }

        public ICommand? EquipmentOverviewCommand { get; set; }

        public ICommand? ArrangeEquipmentCommand { get; set; }

        public ICommand? RoomRenovationCommand { get; set; }

        public string ManagerName
        {
            get => GlobalStore.ReadObject<Manager>("LoggedUser").FirstName;
        }

        public ManagerHomeViewModel ()
        {
            RoomsOverviewCommand = new RoomsOverviewCommand();
            EquipmentOverviewCommand = new EquipmentOverviewCommand();
            ArrangeEquipmentCommand = new ArrangeEquipmentCommand();
            RoomRenovationCommand = new RoomRenovationCommand();
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

            EventBus.RegisterHandler("EquipmentOverview", () =>
            {
                EquipmentOverviewViewModel Eovm = ServiceLocator.Get<EquipmentOverviewViewModel>();
                SwitchCurrentViewModel(Eovm);
            });

            EventBus.RegisterHandler("ArrangeEquipment", () =>
            {
                RoomChoiceViewModel Rcvm = ServiceLocator.Get<RoomChoiceViewModel>();
                SwitchCurrentViewModel(Rcvm);
            });

            EventBus.RegisterHandler("RoomsConfirmed", () =>
            {
                ArrangeEquipmentViewModel Aevm = ServiceLocator.Get<ArrangeEquipmentViewModel>();
                SwitchCurrentViewModel(Aevm);
            });

            EventBus.RegisterHandler("RoomRenovation", () =>
            {
                RoomRenovationViewModel Rrvm = ServiceLocator.Get<RoomRenovationViewModel>();
                SwitchCurrentViewModel(Rrvm);
            });    
        }
    }
}
