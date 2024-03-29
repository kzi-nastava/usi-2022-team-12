﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Ninject;
using HealthInstitution.GUI.Features.Navigation;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.WindowTitle;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.Core.Features.RoomManagement.Repository;

namespace HealthInstitution.GUI.Utility.ViewModel
{
    public class MainViewModel : NavigableViewModel
    {
        private readonly IRoomService _roomService;
        public IRoomService RoomService { get; }
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomRenovationRepository _roomRenovationRepository;
        public IRoomRenovationRepository RoomRenovationRepository { get; }
        public IRoomRepository RoomRepository { get; }

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
        public MainViewModel(LoginViewModel lvm, IRoomService roomService, IRoomRenovationRepository roomRenovationRepository, IRoomRepository roomRepository)
        {
            TitleManager.TitleChanged += OnTitleChanged;
            TitleManager.Title = "Login";
            _roomService = roomService;
            _roomRenovationRepository = roomRenovationRepository;
            _roomRepository = roomRepository;
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
            var renRooms = _roomRenovationRepository.ReadAll();
            foreach (var renRoom in renRooms)
            {
                if (renRoom.EndTime < DateTime.Now)
                {
                    if (renRoom.AdvancedDivide == null)
                    {
                        _roomRenovationRepository.Delete(renRoom.Id);
                    }
                    else if (renRoom.AdvancedDivide == true)
                    {
                        Room room1 = new Room(renRoom.RenovatedRoom.RoomType, renRoom.RenovatedSmallRoom1Name);
                        Room room2 = new Room(renRoom.RenovatedRoom.RoomType, renRoom.RenovatedSmallRoom2Name);
                        foreach (var entry in renRoom.RenovatedRoom.Inventory)
                        {
                            room1.AddEntry(entry);
                        }
                        _roomRepository.Create(room1);
                        _roomRepository.Create(room2);
                        _roomRepository.Delete(renRoom.RenovatedRoom.Id);
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
                            mergedRoom.AddEntry(entry);
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
                                mergedRoom.AddEntry(newEntry);
                            }
                        }
                        _roomRepository.Create(mergedRoom);
                        _roomRepository.Delete(room1.Id);
                        _roomRepository.Delete(room2.Id);
                    }
                }
            }
        }
    }
}
