using HealthInstitution.Model;
using HealthInstitution.Ninject;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace HealthInstitution.ViewModel
{
    public class MainViewModel : NavigableViewModel
    {
        private readonly IRoomService _roomService;
        public IRoomService RoomService { get; }
        private readonly IRoomRenovationService _roomRenovationService;
        public IRoomRenovationService RoomRenovationService { get; }

        private string _viewTitle;
        public string ViewTitle
        {
            get => _viewTitle;
        }
        private void OnTitleChanged()
        {
            _viewTitle = TitleManager.Title;
            OnPropertyChanged(nameof(ViewTitle));
        }
        public LoginViewModel Lvm { get; set; }
        public MainViewModel(LoginViewModel lvm, IRoomService roomService, IRoomRenovationService roomRenovationService)
        {
            TitleManager.TitleChanged += OnTitleChanged;
            TitleManager.Title = "Login";
            _roomService = roomService;
            _roomRenovationService = roomRenovationService;
            updateRoomRenovation();
            Lvm = lvm;
            SwitchCurrentViewModel(lvm);
            RegisterHandler();
        }
        private void RegisterHandler()
        {
            EventBus.RegisterHandler("PatientLogin", () =>
            {
                PatientHomeViewModel Phvm = ServiceLocator.Get<PatientHomeViewModel>();
                SwitchCurrentViewModel(Phvm);
            });

            EventBus.RegisterHandler("SecretaryLogin", () =>
            {
                SecretaryHomeViewModel Shvm = ServiceLocator.Get<SecretaryHomeViewModel>();
                SwitchCurrentViewModel(Shvm);
            });

            EventBus.RegisterHandler("DoctorLogin", () =>
            {
                DoctorHomeViewModel Dhvm = ServiceLocator.Get<DoctorHomeViewModel>();
                SwitchCurrentViewModel(Dhvm);
            });

            EventBus.RegisterHandler("ManagerLogin", () =>
            {
                ManagerHomeViewModel Mhvm = ServiceLocator.Get<ManagerHomeViewModel>();
                SwitchCurrentViewModel(Mhvm);
            });



            EventBus.RegisterHandler("BackToLogin", () =>
            {
                EventBus.Clear();
                ServiceLocator.Reset();
                Lvm = ServiceLocator.Get<LoginViewModel>();
                SwitchCurrentViewModel(Lvm);
                RegisterHandler();
            });
        }

        private void updateRoomRenovation()
        {
            var renRooms = _roomRenovationService.ReadAll();
            foreach (var renRoom in renRooms)
            {
                if (renRoom.EndTime < DateTime.Now)
                {
                    if (renRoom.AdvancedDivide == null)
                    {
                        _roomRenovationService.Delete(renRoom.Id);
                        break;
                    }
                    else if (renRoom.AdvancedDivide == true)
                    {
                        Room room1 = new Room(renRoom.RenovatedRoom.RoomType, renRoom.RenovatedSmallRoom1Name);
                        Room room2 = new Room(renRoom.RenovatedRoom.RoomType, renRoom.RenovatedSmallRoom2Name);
                        foreach (var entry in renRoom.RenovatedRoom.Inventory)
                        {
                            room1.AddEquipment(entry);
                        }
                        _roomService.Create(room1);
                        _roomService.Create(room2);
                        _roomService.Delete(renRoom.RenovatedRoom.Id);
                        break;
                    }
                    else
                    {
                        List<Room> rooms1 = _roomService.ReadRoomsWithName(renRoom.RenovatedSmallRoom1Name).ToList();
                        List<Room> rooms2 = _roomService.ReadRoomsWithName(renRoom.RenovatedSmallRoom2Name).ToList();
                        Room room1 = rooms1[0];
                        Room room2 = rooms2[0];
                        Room mergedRoom = new Room(room1.RoomType, room1.Name + room2.Name + "Merged");
                        foreach (var entry in room1.Inventory)
                        {
                            mergedRoom.AddEquipment(entry);
                        }

                        foreach (var entry in room2.Inventory)
                        {
                            bool itemExisted = false;
                            foreach (var item in mergedRoom.Inventory)
                            {
                                if (entry.Item.Name.Equals(item.Item.Name))
                                {
                                    item.Quantity += 1;
                                    itemExisted = true;
                                    break;
                                }
                            }
                            if (!itemExisted)
                            {
                                Entry<Equipment> newEntry = new Entry<Equipment> { Item = entry.Item, Quantity = 1 };
                                mergedRoom.AddEquipment(newEntry);
                            }
                        }
                        _roomService.Create(mergedRoom);
                        _roomService.Delete(room1.Id);
                        _roomService.Delete(room2.Id);
                        break;
                    }
                }
            }
        }
    }
}
