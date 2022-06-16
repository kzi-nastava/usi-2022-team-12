using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Features.EquipmentManagement.Repository;
using HealthInstitution.Core.Features.OperationsAndExaminations.Commands.DoctorCMD;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.GUI.Utility.Dialog.Service;

namespace HealthInstitution.GUI.Features.EquipmentManagement
{
    public class DynamicEquipmentUpdateViewModel : DialogViewModelBase<DynamicEquipmentUpdateViewModel>
    {
        #region Atributes
        private Entry<Equipment> _selectedItemInventory;
        private Entry<Equipment> _selectedItemUsed;
        private readonly Room _room;
        private readonly IEntryRepository _entryRepository;
        #endregion

        #region Properties
        public IEntryRepository EntryRepository => _entryRepository;
        public Entry<Equipment> SelectedItemInventory
        {
            get => _selectedItemInventory;
            set
            {
                _selectedItemInventory = value;
                OnPropertyChanged(nameof(SelectedItemInventory));
            }
        }
        public Entry<Equipment> SelectedItemUsed
        {
            get => _selectedItemUsed;
            set
            {
                _selectedItemUsed = value;
                OnPropertyChanged(nameof(SelectedItemUsed));
            }
        }
        #endregion

        #region Collections
        private BindingList<Entry<Equipment>> _inventory;
        public BindingList<Entry<Equipment>> Inventory => _inventory;

        private BindingList<Entry<Equipment>> _usedEquipment;
        public BindingList<Entry<Equipment>> UsedEquipment => _usedEquipment;
        #endregion

        #region Commands
        public ICommand FinishCommand { get; }
        public ICommand MoveToUsedCommand { get; }
        public ICommand MoveToInventoryCommand { get; }
        #endregion

        public DynamicEquipmentUpdateViewModel(Room room, IEntryRepository entryRepository) : base("Update dynamic equpment", 800, 650)
        {
            _room = room;
            _entryRepository = entryRepository;
            _inventory = new BindingList<Entry<Equipment>>(room.Inventory.Where(e => e.Item.EquipmentType == EquipmentType.DynamicEquipment).ToList());
            _usedEquipment = new BindingList<Entry<Equipment>>();

            FinishCommand = new FinishDynamicEquipmentUpdateCommand(this);
            MoveToUsedCommand = new MoveToUsedCommand(this);
            MoveToInventoryCommand = new MoveToInventoryCommand(this);
        }

    }
}
