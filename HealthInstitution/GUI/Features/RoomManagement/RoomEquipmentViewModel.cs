using System.Collections.Generic;
using System.Collections.ObjectModel;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.GUI.Utility.Navigation;
using HealthInstitution.GUI.Utility.ViewModel;

namespace HealthInstitution.ViewModel.manager
{
    public class RoomEquipmentViewModel : ViewModelBase
    {

        private readonly ObservableCollection<Entry<Equipment>> _inventory;

        public IEnumerable<Entry<Equipment>> Inventory => _inventory;

        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

        private List<Entry<Equipment>> _entries;

        public List<Entry<Equipment>> Entries
        {
            get => _entries;
            set
            {
                _entries = value;
                OnPropertyChanged(nameof(Entries));
            }
        }

        public RoomEquipmentViewModel()
        {
            SelectedRoom = GlobalStore.ReadObject<Room>("SelectedRoom");
            _inventory = new ObservableCollection<Entry<Equipment>>();
            foreach (var item in SelectedRoom.Inventory)
            {
                _inventory.Add(item);
            }

        }
    }
}
