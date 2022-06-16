using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Core.Features.EquipmentManagement.Commands.ManagerCMD;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Features.EquipmentManagement.Repository;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Repository;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.EquipmentManagement
{
    public class TableModel
    {
        private string _itemName;
        private float _quantity;
        private string _roomName;

        public string ItemName => _itemName;
        public float Quantity => _quantity;
        public string RoomName => _roomName;
        public TableModel(string itemName, float quantity, string roomName)
        {
            this._itemName = itemName;
            this._quantity = quantity;
            this._roomName = roomName;
        }
    }


    public class EquipmentOverviewViewModel : ViewModelBase
    {
        public ICommand? SearchEquipmentCommand { get; }

        public readonly IEntryRepository entryRepository;

        public readonly IRoomRepository roomRepository;

        private readonly ObservableCollection<Room> _roomInventory;

        public IEnumerable<Room> RoomInventory => _roomInventory;

        private readonly ObservableCollection<Entry<Equipment>> _inventory;

        public IEnumerable<Entry<Equipment>> Inventory => _inventory;

        private ObservableCollection<TableModel> _tableModels;

        public IEnumerable<TableModel> TableModels
        {
            get => _tableModels;
        }

        private List<Room> _rooms;
        public List<Room> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }

        private bool _isRoomSelected;
        public bool IsRoomSelected
        {
            get { return _isRoomSelected; }
            set
            {
                if (_isRoomSelected == value) return;

                _isRoomSelected = value;
                OnPropertyChanged(nameof(IsRoomSelected));
            }
        }

        private RoomType _selectedRoomType;
        public RoomType SelectedRoomType
        {
            get => _selectedRoomType;
            set
            {
                _selectedRoomType = value;
                OnPropertyChanged(nameof(SelectedRoomType));
            }
        }

        private List<RoomType> _roomTypes;
        public List<RoomType> RoomTypes
        {
            get => _roomTypes;
            set
            {
                _roomTypes = value;
                OnPropertyChanged(nameof(RoomTypes));
            }
        }

        private bool _isQuantitySelected;
        public bool IsQuantitySelected
        {
            get { return _isQuantitySelected; }
            set
            {
                if (_isQuantitySelected == value) return;

                _isQuantitySelected = value;
                OnPropertyChanged(nameof(IsQuantitySelected));
            }
        }

        private string _selectedQuantity;
        public string SelectedQuantity
        {
            get => _selectedQuantity;
            set
            {
                _selectedQuantity = value;
                OnPropertyChanged(nameof(SelectedQuantity));
            }
        }

        private List<string> _quantityTypes;
        public List<string> QuantityTypes
        {
            get => _quantityTypes;
            set
            {
                _quantityTypes = value;
                OnPropertyChanged(nameof(QuantityTypes));
            }
        }

        private bool _isEquipmentSelected;
        public bool IsEquipmentSelected
        {
            get { return _isEquipmentSelected; }
            set
            {
                if (_isEquipmentSelected == value) return;

                _isEquipmentSelected = value;
                OnPropertyChanged(nameof(IsEquipmentSelected));
            }
        }

        private EquipmentType _selectedEquipmentType;
        public EquipmentType SelectedEquipmentType
        {
            get => _selectedEquipmentType;
            set
            {
                _selectedEquipmentType = value;
                OnPropertyChanged(nameof(SelectedEquipmentType));
            }
        }

        private List<EquipmentType> _equipmentTypes;
        public List<EquipmentType> EquipmentTypes
        {
            get => _equipmentTypes;
            set
            {
                _equipmentTypes = value;
                OnPropertyChanged(nameof(EquipmentTypes));
            }
        }

        private string _searchBox;
        public string SearchBox
        {
            get => _searchBox;
            set
            {
                _searchBox = value;
                OnPropertyChanged(nameof(SearchBox));
            }
        }

        public void loadTable()
        {

            _tableModels.Clear();
            foreach (var room in Rooms)
            {
                foreach (var item in room.Inventory)
                {
                    if (_searchBox.Equals("") || item.Item.Name.ToLower().Contains(_searchBox.ToLower()))
                    {
                        if (IsRoomSelected)
                        {
                            if (!room.RoomType.Equals(SelectedRoomType))
                            {
                                continue;
                            }
                        }
                        if (IsQuantitySelected)
                        {
                            if (_selectedQuantity.Equals("0"))
                            {
                                var quant = item.Quantity;
                                if (quant != 0)
                                {
                                    continue;
                                }
                            }
                            if (_selectedQuantity.Equals("1-10"))
                            {
                                var quant = item.Quantity;
                                if (quant < 1 || quant > 10)
                                {
                                    continue;
                                }
                            }
                            if (_selectedQuantity.Equals("10+"))
                            {
                                var quant = item.Quantity;
                                if (quant < 10)
                                {
                                    continue;
                                }
                            }
                        }
                        if (IsEquipmentSelected)
                        {
                            if (!item.Item.EquipmentType.Equals(SelectedEquipmentType))
                            {
                                continue;
                            }
                        }
                        TableModel tm = new TableModel(item.Item.Name, item.Quantity, room.Name);
                        _tableModels.Add(tm);
                    }
                }
            }
        }

        public EquipmentOverviewViewModel(IRoomRepository roomRepository)
        {
            _searchBox = "";

            SelectedRoomType = RoomType.ExaminationRoom;
            RoomTypes = new List<RoomType>();
            foreach (var type in Enum.GetValues(typeof(RoomType)))
            {
                RoomTypes.Add((RoomType)type);
            }

            SelectedQuantity = "10+";
            QuantityTypes = new List<string>();
            QuantityTypes.Add("10+");
            QuantityTypes.Add("1-10");
            QuantityTypes.Add("0");

            SelectedEquipmentType = EquipmentType.OperationEquipment;
            EquipmentTypes = new List<EquipmentType>();
            foreach (var type in Enum.GetValues(typeof(EquipmentType)))
            {
                EquipmentTypes.Add((EquipmentType)type);
            }

            _tableModels = new ObservableCollection<TableModel>();
            this.roomRepository = roomRepository;
            _inventory = new ObservableCollection<Entry<Equipment>>();
            _roomInventory = new ObservableCollection<Room>();
            Rooms = roomRepository.ReadAll().ToList();
            loadTable();
            SearchEquipmentCommand = new SearchEquipmentCommand(this);

        }

    }
}
