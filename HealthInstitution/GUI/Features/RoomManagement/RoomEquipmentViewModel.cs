using System.Collections.Generic;
using System.Collections.ObjectModel;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.GUI.Features.RoomManagement
{
    public class RoomEquipmentViewModel : ViewModelBase
    {

        #region Attributes

        private readonly ObservableCollection<Entry<Equipment>> _inventory;
        private Room _selectedRoom;
        private List<Entry<Equipment>> _entries;

        #endregion

        #region Properties

        public IEnumerable<Entry<Equipment>> Inventory => _inventory;
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }
        public List<Entry<Equipment>> Entries
        {
            get => _entries;
            set
            {
                _entries = value;
                OnPropertyChanged(nameof(Entries));
            }
        }

        #endregion

        public RoomEquipmentViewModel()
        {
            _selectedRoom = GlobalStore.ReadObject<Room>("SelectedRoom");
            _inventory = new ObservableCollection<Entry<Equipment>>();
            foreach (var item in _selectedRoom.Inventory)
            {
                _inventory.Add(item);
            }
        }
    }
}
