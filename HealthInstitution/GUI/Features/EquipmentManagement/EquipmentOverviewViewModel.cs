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
        #region Attributes

        private readonly ObservableCollection<Room> _roomInventory;
        private readonly ObservableCollection<Entry<Equipment>> _inventory;
        private ObservableCollection<TableModel> _tableModels;
        private List<Room> _rooms;
        private bool _isRoomSelected;
        private RoomType _selectedRoomType;
        private List<RoomType> _roomTypes;
        private bool _isQuantitySelected;
        private string _selectedQuantity;
        private List<string> _quantityTypes;
        private bool _isEquipmentSelected;
        private EquipmentType _selectedEquipmentType;
        private List<EquipmentType> _equipmentTypes;
        private string _searchBox;
        public readonly IEntryRepository entryRepository;
        public readonly IRoomRepository roomRepository;

        #endregion

        #region Properties

        public IEnumerable<Room> RoomInventory => _roomInventory;
        public IEnumerable<Entry<Equipment>> Inventory => _inventory;
        public IEnumerable<TableModel> TableModels
        {
            get => _tableModels;
        }
        public List<Room> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }
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
        public RoomType SelectedRoomType
        {
            get => _selectedRoomType;
            set
            {
                _selectedRoomType = value;
                OnPropertyChanged(nameof(SelectedRoomType));
            }
        }
        public List<RoomType> RoomTypes
        {
            get => _roomTypes;
            set
            {
                _roomTypes = value;
                OnPropertyChanged(nameof(RoomTypes));
            }
        }
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
        public string SelectedQuantity
        {
            get => _selectedQuantity;
            set
            {
                _selectedQuantity = value;
                OnPropertyChanged(nameof(SelectedQuantity));
            }
        }
        public List<string> QuantityTypes
        {
            get => _quantityTypes;
            set
            {
                _quantityTypes = value;
                OnPropertyChanged(nameof(QuantityTypes));
            }
        }
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
        public EquipmentType SelectedEquipmentType
        {
            get => _selectedEquipmentType;
            set
            {
                _selectedEquipmentType = value;
                OnPropertyChanged(nameof(SelectedEquipmentType));
            }
        }
        public List<EquipmentType> EquipmentTypes
        {
            get => _equipmentTypes;
            set
            {
                _equipmentTypes = value;
                OnPropertyChanged(nameof(EquipmentTypes));
            }
        }
        public string SearchBox
        {
            get => _searchBox;
            set
            {
                _searchBox = value;
                OnPropertyChanged(nameof(SearchBox));
            }
        }

        #endregion

        #region Commands

        public ICommand? SearchEquipmentCommand { get; }

        #endregion

        #region Methods

        public void LoadTable()
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
        private void LoadComboBoxes()
        {
            _searchBox = "";

            _roomTypes = new List<RoomType>();
            foreach (var type in Enum.GetValues(typeof(RoomType)))
            {
                _roomTypes.Add((RoomType)type);
            }
            _selectedRoomType = _roomTypes[0];

            
            _quantityTypes = new List<string>();
            _quantityTypes.Add("10+");
            _quantityTypes.Add("1-10");
            _quantityTypes.Add("0");
            _selectedQuantity = _quantityTypes[0];

            
            _equipmentTypes = new List<EquipmentType>();
            foreach (var type in Enum.GetValues(typeof(EquipmentType)))
            {
                _equipmentTypes.Add((EquipmentType)type);
            }
            _selectedEquipmentType = _equipmentTypes[0];
        }

        #endregion


        public EquipmentOverviewViewModel(IRoomRepository roomRepository)
        {
            _tableModels = new ObservableCollection<TableModel>();
            _inventory = new ObservableCollection<Entry<Equipment>>();
            _roomInventory = new ObservableCollection<Room>();
            _rooms = roomRepository.ReadAll().ToList();
            this.roomRepository = roomRepository;

            LoadComboBoxes();
            LoadTable();

            SearchEquipmentCommand = new SearchEquipmentCommand(this);

        }

    }
}
