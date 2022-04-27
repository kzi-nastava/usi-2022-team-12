using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.ViewModel
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
