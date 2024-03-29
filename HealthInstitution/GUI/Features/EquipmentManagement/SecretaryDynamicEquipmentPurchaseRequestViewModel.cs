﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HealthInstitution.Core.Features.EquipmentManagement.Model;
using HealthInstitution.Core.Features.EquipmentManagement.Repository;
using HealthInstitution.Core.Features.EquipmentManagement.Service;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.Core.Utility.HelperClasses;
using HealthInstitution.GUI.Features.EquipmentManagement.Dialog;
using HealthInstitution.GUI.Utility.Dialog.Service;

namespace HealthInstitution.GUI.Features.EquipmentManagement
{
    public class SecretaryDynamicEquipmentPurchaseRequestViewModel : ObservableEntity
    {
        #region Properties

        private string _searchText;
        public string SearchText { get => _searchText; set => OnPropertyChanged(ref _searchText, value); }

        private ObservableCollection<Equipment> _dynamicEquipments;
        public ObservableCollection<Equipment> DynamicEquipments
        {
            get { return _dynamicEquipments; }
            set { OnPropertyChanged(ref _dynamicEquipments, value); }
        }

        private Equipment _selectedDynamicEquipment;
        public Equipment SelectedDynamicEquipment
        {
            get { return _selectedDynamicEquipment; }
            set { OnPropertyChanged(ref _selectedDynamicEquipment, value); }
        }

        #endregion

        #region Services

        private readonly IDialogService _dialogService;

        private readonly IEquipmentService _equipmentService;

        private readonly IEquipmentPurchaseRequestService _equipmentPurchaseRequestService;

        private readonly IRoomService _roomService;

        #endregion

        #region Commands

        public ICommand SearchCommand { get; private set; }
        public ICommand MakeRequest { get; private set; }

        #endregion

        public SecretaryDynamicEquipmentPurchaseRequestViewModel(IDialogService dialogService, IEquipmentService equipmentService, IEquipmentPurchaseRequestService equipmentPurchaseRequestService,
            IRoomService roomService)
        {
            _dialogService = dialogService;
            _equipmentService = equipmentService;
            _equipmentPurchaseRequestService = equipmentPurchaseRequestService;
            _roomService = roomService;

            DynamicEquipments = new ObservableCollection<Equipment>(_equipmentService.GetEquipmentNotInRoom(_roomService.GetStorage(), EquipmentType.DynamicEquipment));

            MakeRequest = new RelayCommand(() =>
            {
                QuantitySelectViewModel quantitySelectVM = new QuantitySelectViewModel();
                Tuple<int, bool?> chosenQuantity = _dialogService.OpenDialogWithReturnType(quantitySelectVM);

                // Dialog was closed
                if (chosenQuantity.Item2 == false)
                    return;

                var purchaseRequest = new EquipmentPurchaseRequest()
                {
                    PurchasedEquipment = new Entry<Equipment>() { Item = SelectedDynamicEquipment, Quantity = chosenQuantity.Item1 },
                    DateOfTransfer = DateTime.Now.AddDays(1),
                    IsDone = false
                };

                _equipmentPurchaseRequestService.Create(purchaseRequest);

                MessageBox.Show("Purchase request made successfully.");


            }, () => _selectedDynamicEquipment != null);

            SearchCommand = new RelayCommand(Search);

        }

        private void UpdatePage()
        {
            DynamicEquipments = new ObservableCollection<Equipment>(_equipmentService.GetEquipmentNotInRoom(_roomService.GetStorage(), EquipmentType.DynamicEquipment));
        }

        private void Search()
        {
            if (SearchText == "" || SearchText == null)
                UpdatePage();
            else
                DynamicEquipments = new ObservableCollection<Equipment>(_equipmentService.FilterEquipmentNotInRoomBySearchText(_roomService.GetStorage(), EquipmentType.DynamicEquipment, SearchText));
        }
    }
}
