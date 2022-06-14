using HealthInstitution.Model;
using System;

namespace HealthInstitution.Core.Features.EquipmentManagement.Model
{
    public class EquipmentPurchaseRequest : BaseObservableEntity
    {
        #region Attributes
        private Entry<Equipment> _purchasedEquipment;
        public virtual Entry<Equipment> PurchasedEquipment { get => _purchasedEquipment; set => OnPropertyChanged(ref _purchasedEquipment, value); }

        private DateTime _dateOfTransfer;
        public DateTime DateOfTransfer { get => _dateOfTransfer; set => OnPropertyChanged(ref _dateOfTransfer, value); }

        private bool _isDone;
        public bool IsDone { get => _isDone; set => OnPropertyChanged(ref _isDone, value); }

        #endregion

        #region Constructor

        public EquipmentPurchaseRequest() { }

        public EquipmentPurchaseRequest(Room destinationRoom, DateTime dateOfTransfer, Entry<Equipment> purchasedEquipment)
        {
            _purchasedEquipment = purchasedEquipment;
            _dateOfTransfer = dateOfTransfer;
        }

        #endregion
    }
}
