using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Utility.HelperClasses;
using HealthInstitution.Dialogs.Custom;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.Utility;

namespace HealthInstitution.ViewModel.secretary
{
    public class SecretaryDynamicEquipmentArrangementViewModel : ObservableEntity
    {
        #region Properties

        private string _searchText;
        public string SearchText { get => _searchText; set => OnPropertyChanged(ref _searchText, value); }

        private ObservableCollection<Room> _priorityRooms;
        public ObservableCollection<Room> PriorityRooms
        {
            get { return _priorityRooms; }
            set { OnPropertyChanged(ref _priorityRooms, value); }
        }

        private Room? _selectedPriorityRoom;
        public Room? SelectedPriorityRoom
        {
            get { return _selectedPriorityRoom; }
            set { OnPropertyChanged(ref _selectedPriorityRoom, value); }
        }

        #endregion

        #region Services

        private readonly IDialogService _dialogService;

        private readonly IRoomService _roomService;

        private readonly IEquipmentService _equipmentService;

        #endregion

        #region Commands

        public ICommand SearchCommand { get; private set; }

        public ICommand Transfer { get; private set; }

        #endregion

        public SecretaryDynamicEquipmentArrangementViewModel(IDialogService dialogService, IRoomService roomService, IEquipmentService equipmentService)
        {
            _dialogService = dialogService;
            _roomService = roomService;
            _equipmentService = equipmentService;

            Transfer = new RelayCommand(() =>
            {
                var equipmentTransferVM = new EquipmentTransferViewModel(_selectedPriorityRoom, _roomService, _equipmentService);
                _dialogService.OpenDialog(equipmentTransferVM);
                UpdatePage();
            },
                () => SelectedPriorityRoom != null);

            SearchCommand = new RelayCommand(Search);

            UpdatePage();
        }

        private void UpdatePage()
        {
            PriorityRooms = new ObservableCollection<Room>(_roomService.GetRoomsLowOnEquipment());
        }

        private void Search()
        {
            if (SearchText is "" or null)
                UpdatePage();
            else
                PriorityRooms = new ObservableCollection<Room>(_roomService.FilterRoomsLowOnEquipment(SearchText));
        }
    }
}
