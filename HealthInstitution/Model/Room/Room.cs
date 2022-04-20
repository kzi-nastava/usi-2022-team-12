﻿using System;
using System.Collections.Generic;

namespace HealthInstitution.Model
{
    public class Room : BaseObservableEntity
    {
        #region Attributes

        private RoomType _roomType;
        public RoomType RoomType { get => _roomType; set => OnPropertyChanged(ref _roomType, value); }

        private readonly IList<Entry<Equipment>> _inventory;
        public virtual IList<Entry<Equipment>> Inventory { get => _inventory;}

        #endregion

        #region Constructor

        protected Room(RoomType roomType)
        {
            _roomType = roomType;
            _inventory = new List<Entry<Equipment>>();
        }

        #endregion

        #region Methods

        public void AddEquipment(Entry<Equipment> entry)
        {
            foreach (Entry<Equipment> includedEntry in _inventory)
            {
                if (includedEntry.Item.Id == entry.Item.Id)
                {
                    // Baci exception
                    return;
                }
            }
            
            _inventory.Add(entry);
        }

        #endregion
    }
}