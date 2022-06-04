using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.Model;
using HealthInstitution.Model.room;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;

namespace HealthInstitution.Dialogs.Custom
{
    public class EntryView : BaseObservableEntity
    {
        private Guid _equipmentId;
        public Guid EquipmentId
        {
            get { return _equipmentId; }
            set { OnPropertyChanged(ref _equipmentId, value); }
        }

        private string _equipmentName;
        public string EquipmentName
        {
            get { return _equipmentName; }
            set { OnPropertyChanged(ref _equipmentName, value); }
        }

        private float _quantity;
        public float Quantity
        {
            get { return _quantity; }
            set { OnPropertyChanged(ref _quantity, value); }
        }

        private bool _important;
        public bool Important
        {
            get { return _important; }
            set { OnPropertyChanged(ref _important, value); }
        }
    }

    public class EquipmentTransferViewModel : DialogViewModelBase<EquipmentTransferViewModel>
    {
        #region Properties

        private Room? _firstRoom;
        public Room? FirstRoom
        {
            get { return _firstRoom; }
            set { OnPropertyChanged(ref _firstRoom, value); }
        }

        private ObservableCollection<EntryView>? _filteredFirstRoomEquipment;
        public ObservableCollection<EntryView>? FilteredFirstRoomEquipment
        {
            get { return _filteredFirstRoomEquipment; }
            set
            {
                OnPropertyChanged(ref _filteredFirstRoomEquipment, value);
            }
        }

        private EntryView? _selectedFirstEquipment;
        public EntryView? SelectedFirstEquipment
        {
            get { return _selectedFirstEquipment; }
            set { OnPropertyChanged(ref _selectedFirstEquipment, value); }
        }

        private Room? _secondRoom;
        public Room? SecondRoom
        {
            get { return _secondRoom; }
            set
            {
                OnPropertyChanged(ref _secondRoom, value);
            }
        }

        private ObservableCollection<EntryView>? _filteredSecondRoomEquipment;
        public ObservableCollection<EntryView>? FilteredSecondRoomEquipment
        {
            get { return _filteredSecondRoomEquipment; }
            set
            {
                OnPropertyChanged(ref _filteredSecondRoomEquipment, value);
            }
        }

        private EntryView? _selectedSecondEquipment;
        public EntryView? SelectedSecondEquipment
        {
            get { return _selectedSecondEquipment; }
            set { OnPropertyChanged(ref _selectedSecondEquipment, value); }
        }

        private ObservableCollection<Room> _rooms;
        public ObservableCollection<Room> Rooms
        {
            get { return _rooms; }
            set { OnPropertyChanged(ref _rooms, value); }
        }

        private Room? _selectedRoom;
        public Room? SelectedRoom
        {
            get { return _selectedRoom; }
            set { OnPropertyChanged(ref _selectedRoom, value); }
        }

        private bool _canChange;

        public bool CanChange
        {
            get { return _canChange; }
            set { OnPropertyChanged(ref _canChange, value); }
        }

        private bool _canConfirm;

        public bool CanConfirm
        {
            get { return _canConfirm; }
            set { OnPropertyChanged(ref _canConfirm, value); }
        }

        #endregion

        #region Services

        public readonly IRoomService _roomService;

        public readonly IEquipmentService _equipmentService;

        #endregion

        #region Commands

        public ICommand ChangeRoom { get; private set; }

        public ICommand FirstRoomToSecond { get; private set; }

        public ICommand SecondRoomToFirst { get; private set; }

        public ICommand Confirm { get; private set; }

        #endregion

        public EquipmentTransferViewModel(Room firstRoom, IRoomService roomService, IEquipmentService equipmentService) :
            base("Dynamic equipment arrangement", 1000, 600)
        {
            FirstRoom = firstRoom;
            _roomService = roomService;
            _equipmentService = equipmentService;
            CanChange = true;
            CanConfirm = false;

            FetchRooms();

            FilterRoomEquipment();

            InstantiateCommands();
        }

        public void FetchRooms()
        {
            var requiredTypes = new List<RoomType>()
            {
                RoomType.ExaminationRoom,
                RoomType.OperationRoom
            };
            Rooms = new ObservableCollection<Room>(_roomService.ReadRooms(requiredTypes).Where(room => room.Id != FirstRoom.Id));
            Rooms.Add(_roomService.GetStorage());
        }

        #region Command instantiate

        public void InstantiateCommands()
        {
            InstantiateChangeRoomCommand();
            InstantiateTransferCommands();
            InstantiateConfirmCommand();
        }

        public void InstantiateChangeRoomCommand()
        {
            ChangeRoom = new RelayCommand(() =>
            {
                SecondRoom = _roomService.Read(SelectedRoom.Id);
                FilterRoomEquipment();
                CanConfirm = false;
            }, () => CanChange && SelectedRoom != null);
        }

        public void InstantiateTransferCommands()
        {
            FirstRoomToSecond = new RelayCommand(() =>
            {
                DecreaseEquipmentQuantity(FilteredFirstRoomEquipment, SelectedFirstEquipment);
                IncreaseEquipmentQuantity(FilteredSecondRoomEquipment, SelectedFirstEquipment);
                CanChange = false;
                CanConfirm = true;
            }, () => CanExecuteTransfer(0));

            SecondRoomToFirst = new RelayCommand(() =>
            {
                DecreaseEquipmentQuantity(FilteredSecondRoomEquipment, SelectedSecondEquipment);
                IncreaseEquipmentQuantity(FilteredFirstRoomEquipment, SelectedSecondEquipment);
                CanChange = false;
                CanConfirm = true;
            }, () => CanExecuteTransfer(1));
        }

        public void InstantiateConfirmCommand()
        {
            Confirm = new RelayCommand(() =>
            {
                ConfirmChanges(FirstRoom, FilteredFirstRoomEquipment);
                ConfirmChanges(SecondRoom, FilteredSecondRoomEquipment);
                CanChange = true;

                MessageBox.Show("Transfer successfully completed.");
                FilterRoomEquipment();
            }, () => CanConfirm);
        }

        public void ConfirmChanges(Room room, ObservableCollection<EntryView> entries)
        {
            foreach (var entry in entries)
            {
                var entryToUpdate = room.Inventory.FirstOrDefault(e => e.Item.Id == entry.EquipmentId);

                if (entryToUpdate == null && entry.Quantity != 0)
                    room.Inventory.Add(new Entry<Equipment>()
                    {
                        Item = _equipmentService.Read(entry.EquipmentId),
                        Quantity = entry.Quantity
                    });
                else if (entryToUpdate != null)
                    entryToUpdate.Quantity = entry.Quantity;
            }

            room.RefreshInventory();
            _roomService.Update(room);
        }

        public bool CanExecuteTransfer(int direction)
        {
            if (FirstRoom == null)
                return false;

            if (SecondRoom == null)
                return false;

            return direction switch
            {
                0 when SelectedFirstEquipment == null => false,
                0 when SelectedFirstEquipment.Quantity == 0 => false,
                1 when SelectedSecondEquipment == null => false,
                1 when SelectedSecondEquipment.Quantity == 0 => false,
                _ => true
            };
        }

        #endregion

        #region Equipment quantity logic

        public void IncreaseEquipmentQuantity(ObservableCollection<EntryView> equipment, EntryView? newEntry)
        {
            var entryToUpdate = equipment.FirstOrDefault(e => e.EquipmentId == newEntry.EquipmentId);
            if (entryToUpdate != null)
                entryToUpdate.Quantity += 1;
            else
                equipment.Add(new EntryView()
                {
                    EquipmentId = newEntry.EquipmentId,
                    EquipmentName = newEntry.EquipmentName,
                    Quantity = 1
                });
        }

        public void DecreaseEquipmentQuantity(ObservableCollection<EntryView> equipment, EntryView? newEntry)
        {
            var entryToUpdate = equipment.FirstOrDefault(e => e.EquipmentId == newEntry.EquipmentId);
            if (entryToUpdate != null)
                entryToUpdate.Quantity -= 1;
        }

        #endregion

        #region Equipment filter

        public void FilterRoomEquipment()
        {
            FilterFirstRoomEquipment();
            FilterSecondRoomEquipment();
        }

        public void FilterFirstRoomEquipment()
        {
            FilteredFirstRoomEquipment = new ObservableCollection<EntryView>();
            if (FirstRoom == null)
            {
                return;
            }

            var filteredEntries = FirstRoom.Inventory.Where(entry => entry.Item.EquipmentType == EquipmentType.DynamicEquipment).ToList();
            foreach (var entry in filteredEntries)
                FilteredFirstRoomEquipment.Add(new EntryView()
                {
                    EquipmentId = entry.Item.Id,
                    EquipmentName = entry.Item.Name,
                    Quantity = entry.Quantity,
                    Important = false
                });
        }

        public void FilterSecondRoomEquipment()
        {
            FilteredSecondRoomEquipment = new ObservableCollection<EntryView>();
            if (SecondRoom == null)
            {
                return;
            }

            var filteredEntries = SecondRoom.Inventory.Where(entry => entry.Item.EquipmentType == EquipmentType.DynamicEquipment).ToList();
            bool isImportant;
            foreach (var entry in filteredEntries)
            {
                isImportant = FilteredFirstRoomEquipment.All(entryView => entryView.EquipmentId != entry.Item.Id || entryView.Quantity == 0);

                FilteredSecondRoomEquipment.Add(new EntryView()
                {
                    EquipmentId = entry.Item.Id,
                    EquipmentName = entry.Item.Name,
                    Quantity = entry.Quantity,
                    Important = isImportant
                });
            }
        }

        #endregion
    }
}
