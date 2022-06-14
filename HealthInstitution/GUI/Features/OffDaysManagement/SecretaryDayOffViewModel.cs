using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HealthInstitution.Core.Features.OffDaysManagement.Model;
using HealthInstitution.Core.Utility.HelperClasses;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.GUI.Features.OffDaysManagement.Dialog;
using HealthInstitution.Model.patient;
using HealthInstitution.Ninject;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.Utility;

namespace HealthInstitution.ViewModel.secretary
{
    public class SecretaryDayOffViewModel : ObservableEntity
    {
        #region Properties

        private string _searchText;
        public string SearchText { get => _searchText; set => OnPropertyChanged(ref _searchText, value); }

        private ObservableCollection<OffDaysRequest> _offDaysRequests;
        public ObservableCollection<OffDaysRequest> OffDaysRequests
        {
            get { return _offDaysRequests; }
            set { OnPropertyChanged(ref _offDaysRequests, value); }
        }

        private OffDaysRequest? _selectedOffDaysRequest;
        public OffDaysRequest? SelectedOffDaysRequest
        {
            get { return _selectedOffDaysRequest; }
            set { OnPropertyChanged(ref _selectedOffDaysRequest, value); }
        }

        #endregion

        #region Services

        private readonly IDialogService _dialogService;

        private readonly IOffDaysRequestService _offDaysRequestService;

        private readonly INotificationService _notificationService;

        #endregion

        #region Commands

        public ICommand SearchCommand { get; private set; }

        public ICommand Accept { get; private set; }

        public ICommand Reject { get; private set; }

        #endregion

        public SecretaryDayOffViewModel(IDialogService dialogService, IOffDaysRequestService offDaysRequestService,
            INotificationService notificationService)
        {
            _dialogService = dialogService;
            _offDaysRequestService = offDaysRequestService;
            _notificationService = notificationService;

            UpdatePage();

            SearchCommand = new RelayCommand(Search);

            Accept = new RelayCommand(() =>
            {
                _offDaysRequestService.AcceptRequest(SelectedOffDaysRequest.Id);
                MessageBox.Show("Request accepted successfully.");
                _notificationService.CreateNotification(SelectedOffDaysRequest.Doctor.Id, "Your off days request has been accepted.");
                UpdatePage();
            },
                () => SelectedOffDaysRequest != null);

            Reject = new RelayCommand(() =>
            {
                var (refuseComment, isFinished) = _dialogService.OpenDialogWithReturnType(ServiceLocator.Get<OffDaysDeclineReasonViewModel>());
                if ((bool)isFinished)
                {
                    _offDaysRequestService.DeclineRequest(SelectedOffDaysRequest.Id, refuseComment);
                    MessageBox.Show("Request rejected successfully.");
                    _notificationService.CreateNotification(SelectedOffDaysRequest.Doctor.Id, "Your off days request has been refused.");
                }
                UpdatePage();
            },
                () => SelectedOffDaysRequest != null);

        }

        private void UpdatePage()
        {
            OffDaysRequests = new ObservableCollection<OffDaysRequest>(_offDaysRequestService.GetPendingOffDaysRequests());
        }

        private void Search()
        {
            if (SearchText is "" or null)
                UpdatePage();
            else
                OffDaysRequests = new ObservableCollection<OffDaysRequest>(_offDaysRequestService.FilterPendingOffDaysRequestsForSearchText(SearchText));
        }

    }
}
