using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Core.Features.EquipmentManagement.Model
{
    /// <summary>
    /// Maps entity to value that refers its quantity;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Entry<T> : BaseObservableEntity where T : BaseObservableEntity
    {
        #region Attributes

        private T _item;
        public virtual T Item { get => _item; set => OnPropertyChanged(ref _item, value); }

        private float _quantity;
        public float Quantity { get => _quantity; set => OnPropertyChanged(ref _quantity, value); }

        #endregion

        public Entry(Entry<T> other)
        {
            _item = other._item;
            _quantity = other._quantity;
            Id = other.Id;
            CreatedAt = other.CreatedAt;
            IsActive = other.IsActive;
        }

        public Entry()
        {

        }
    }
}
