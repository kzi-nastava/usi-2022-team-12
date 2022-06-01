﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthInstitution.Model
{
    public class Room : BaseObservableEntity
    {
        #region Attributes
        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        private RoomType _roomType;
        public RoomType RoomType { get => _roomType; set => OnPropertyChanged(ref _roomType, value); }

        private readonly IList<Entry<Equipment>> _inventory;
        public virtual IList<Entry<Equipment>> Inventory { get => _inventory; }

        #endregion

        #region Constructor

        public Room(RoomType roomType, string name)
        {
            _name = name;
            _roomType = roomType;
            _inventory = new List<Entry<Equipment>>();
        }

        #endregion

        #region Methods

        public void AddEquipment(Equipment newEquipment)
        {
            if (HasEquipment(newEquipment))
            {
                return;
            }

            _inventory.Add(GetEntry(newEquipment, 0));
        }

        public void AddEquipment(IList<Equipment> newEquipment)
        {
            foreach (var equipment in newEquipment)
                AddEquipment(equipment);
        }

        public void AddEntry(Entry<Equipment> newEntry)
        {
            if (HasEquipment(newEntry.Item))
            {
                return;
            }

            _inventory.Add(newEntry);
        }

        public void IncreaseEquipmentQuantity(Entry<Equipment> deliveredEquipment)
        {
            var entryToUpdate = Inventory.FirstOrDefault(e => e.Item.Id == deliveredEquipment.Item.Id);
            if (entryToUpdate != null)
                entryToUpdate.Quantity += deliveredEquipment.Quantity;
        }

        public bool ContainsEquipment(Equipment equipment)
        {
            return _inventory.Any(equipmentEntry => equipmentEntry.Item.Id == equipment.Id);
        }

        public bool IsLowOnEquipment()
        {
            return _inventory.Any(equipmentEntry => equipmentEntry.Quantity < 5);
        }

        public IList<Equipment> GetMissingEquipment(IList<Equipment> requiredEquipment)
        {
            return requiredEquipment.Where(equipment => _inventory.All(equipmentEntry => equipmentEntry.Item.Id != equipment.Id))
                                    .ToList();
        }

        public bool HasEquipment(Equipment newEquipment)
        {
            return _inventory.Any(includedEntry => includedEntry.Item.Id == newEquipment.Id);
        }

        private Entry<Equipment> GetEntry(Equipment newEquipment, int quantity)
        {
            return new Entry<Equipment>
            {
                Item = newEquipment,
                Quantity = quantity
            };
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
