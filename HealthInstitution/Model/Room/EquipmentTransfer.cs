using System;
using System.Collections.Generic;

namespace HealthInstitution.Model.room
{
    public class EquipmentTransfer : BaseObservableEntity
    {
        #region Attributes

        private Room _originRoom;
        public virtual Room OriginRoom { get => _originRoom; set => OnPropertyChanged(ref _originRoom, value); }

        private Room _destinationRoom;
        public virtual Room DestinationRoom { get => _destinationRoom; set => OnPropertyChanged(ref _destinationRoom, value); }

        private DateTime _dateOfTransfer;
        public DateTime DateOfTransfer { get => _dateOfTransfer; set => OnPropertyChanged(ref _dateOfTransfer, value); }

        private readonly IList<Entry<Equipment>> _transferredEquipment;
        public virtual IList<Entry<Equipment>> TransferredEquipment { get => _transferredEquipment; }

        #endregion

        #region Constructor

        public EquipmentTransfer() { }
        public EquipmentTransfer(Room originRoom, Room destinationRoom, DateTime dateOfTransfer)
        {
            _originRoom = originRoom;
            _destinationRoom = destinationRoom;
            _dateOfTransfer = dateOfTransfer;
            _transferredEquipment = new List<Entry<Equipment>>();
        }

        #endregion

        #region Methods

        public void AddEquipment(Equipment equipmentToAdd, float quantity)
        {
            foreach (Entry<Equipment> equipment in TransferredEquipment)
            {
                if (equipment.Id == equipmentToAdd.Id)
                {
                    equipment.Quantity = quantity;
                    return;
                }
            }

            _transferredEquipment.Add(new Entry<Equipment> { Id = equipmentToAdd.Id, Quantity = quantity });
        }
        #endregion
    }
}
