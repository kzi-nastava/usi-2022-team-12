using HealthInstitution.Commands;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using HealthInstitution.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.Commands.doctor;
using HealthInstitution.Model.room;

namespace HealthInstitution.Dialogs.Custom.Doctor
{
    public class DynamicEquipmentUpdateViewModel : DialogViewModelBase<DynamicEquipmentUpdateViewModel>
    {
        #region Atributes
        private Entry<Equipment> _selectedItemInventory;
        private Entry<Equipment> _selectedItemUsed;
        private readonly Room _room;
        private readonly IEntryService _entryService;
        #endregion

        #region Properties
        public IEntryService EntryService => _entryService;
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

        public DynamicEquipmentUpdateViewModel(Room room, IEntryService entryService): base("Update dynamic equpment", 800, 650)
        {
            _room = room;
            _entryService = entryService;
            _inventory = new BindingList<Entry<Equipment>>(room.Inventory.Where(e => e.Item.EquipmentType == EquipmentType.DynamicEquipment).ToList());
            _usedEquipment= new BindingList<Entry<Equipment>>();

            FinishCommand = new FinishDynamicEquipmentUpdateCommand(this);
            MoveToUsedCommand = new MoveToUsedCommand(this);
            MoveToInventoryCommand = new MoveToInventoryCommand(this);
        }

    }
}
