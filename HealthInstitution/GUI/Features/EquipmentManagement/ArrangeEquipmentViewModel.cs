using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using HealthInstitution.Core.Features.EquipmentManagement.Commands.ManagerCMD;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Services;
using HealthInstitution.Core.Services.Interfaces;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.EquipmentManagement
{
    public class ArrangeEquipmentViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Entry<Equipment>> _inventory1;
        private readonly ObservableCollection<Entry<Equipment>> _inventory2;

        public readonly IEntryService _entryService;
        public readonly IRoomService _roomService;


        public IEnumerable<Entry<Equipment>> Inventory1Binding
        {
            get
            {
                return _inventory1.Where(e => e.Quantity > 0);
            }
        }
        public IEnumerable<Entry<Equipment>> Inventory2Binding
        {
            get
            {
                return _inventory2.Where(e => e.Quantity > 0);
            }
        }

        public IEnumerable<Entry<Equipment>> Inventory1 => _inventory1;
        public IEnumerable<Entry<Equipment>> Inventory2 => _inventory2;

        public ICommand? FirstToSecondCommand { get; }
        public ICommand? SecondToFirstCommand { get; }
        public ICommand? ConfirmArrangementCommand { get; }


        private string _roomName1;
        public string? RoomName1
        {
            get => _roomName1;
            set
            {
                _roomName1 = value;
                OnPropertyChanged(nameof(RoomName1));
            }
        }

        private string _roomName2;
        public string? RoomName2
        {
            get => _roomName2;
            set
            {
                _roomName2 = value;
                OnPropertyChanged(nameof(RoomName2));
            }
        }

        private Room _room1;
        public Room? Room1
        {
            get => _room1;
            set
            {
                _room1 = value;
                OnPropertyChanged(nameof(Room1));
            }
        }

        private Room _room2;
        public Room? Room2
        {
            get => _room2;
            set
            {
                _room2 = value;
                OnPropertyChanged(nameof(Room2));
            }
        }

        private Entry<Equipment> _selectedEntry1;
        public Entry<Equipment>? SelectedEntry1
        {
            get => _selectedEntry1;
            set
            {
                _selectedEntry1 = value;
                OnPropertyChanged(nameof(SelectedEntry1));
            }
        }

        private Entry<Equipment> _selectedEntry2;
        public Entry<Equipment>? SelectedEntry2
        {
            get => _selectedEntry2;
            set
            {
                _selectedEntry2 = value;
                OnPropertyChanged(nameof(SelectedEntry2));
            }
        }

        public void MoveItemBetweenRooms(Entry<Equipment> selectedEntry,
            ObservableCollection<Entry<Equipment>> senderInventory,
            ObservableCollection<Entry<Equipment>> receiverInventory)
        {

            if (senderInventory.Count() == 0)
            {
                MessageBox.Show("Chosen room doesn't have any equipment!");
                return;
            }
            if (selectedEntry == null)
            {
                selectedEntry = senderInventory.FirstOrDefault();
            }
            foreach (var item in senderInventory)
            {
                if (selectedEntry.Item.Name.Equals(item.Item.Name))
                {
                    item.Quantity -= 1;
                    break;
                }
            }
            bool itemExisted = false;
            foreach (var item in receiverInventory)
            {
                if (selectedEntry.Item.Name.Equals(item.Item.Name))
                {
                    item.Quantity += 1;
                    itemExisted = true;
                    break;
                }
            }
            if (!itemExisted)
            {
                Entry<Equipment> newEntry = new Entry<Equipment> { Item = selectedEntry.Item, Quantity = 1 };
                receiverInventory.Add(newEntry);
            }

            OnPropertyChanged(nameof(Inventory1Binding));
            OnPropertyChanged(nameof(Inventory2Binding));

        }

        public ArrangeEquipmentViewModel(IEntryService entryService, IRoomService roomService)
        {
            _room1 = GlobalStore.ReadObject<Room>("ArrangeRoom1");
            _room2 = GlobalStore.ReadObject<Room>("ArrangeRoom2");
            _roomName1 = _room1.Name;
            _roomName2 = _room2.Name;
            _inventory1 = new ObservableCollection<Entry<Equipment>>();
            _inventory2 = new ObservableCollection<Entry<Equipment>>();
            foreach (var item in _room1.Inventory)
            {
                if (item.Quantity != 0)
                {
                    Entry<Equipment> entryHelper = new Entry<Equipment> { Item = item.Item, Quantity = item.Quantity };
                    _inventory1.Add(entryHelper);
                }

            }
            foreach (var item in _room2.Inventory)
            {
                if (item.Quantity != 0)
                {
                    Entry<Equipment> entryHelper = new Entry<Equipment> { Item = item.Item, Quantity = item.Quantity };
                    _inventory2.Add(entryHelper);
                }
            }
            _selectedEntry1 = _inventory1.FirstOrDefault();
            _selectedEntry2 = _inventory2.FirstOrDefault();
            FirstToSecondCommand = new FirstToSecondCommand(this);
            SecondToFirstCommand = new SecondToFirstCommand(this);
            _entryService = entryService;
            _roomService = roomService;
            ConfirmArrangementCommand = new ConfirmArrangementCommand(this);

        }
    }
}
