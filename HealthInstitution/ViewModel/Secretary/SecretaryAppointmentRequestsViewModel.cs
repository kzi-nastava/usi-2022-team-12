using HealthInstitution.Dialogs.Custom;
using HealthInstitution.Dialogs.Service;
using HealthInstitution.Model;
using HealthInstitution.Services.Intefaces;
using HealthInstitution.Utility;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    public class SecretaryAppointmentRequestsViewModel : ObservableEntity
    {
        #region Properties

        private string _searchText;
        public string SearchText { get => _searchText; set => OnPropertyChanged(ref _searchText, value); }

        private ObservableCollection<AppointmentRequest> _appointmentRequests;
        public ObservableCollection<AppointmentRequest> AppointmentRequests
        {
            get { return _appointmentRequests; }
            set { OnPropertyChanged(ref _appointmentRequests, value); }
        }

        private AppointmentRequest _selectedAppointmentRequest;
        public AppointmentRequest SelectedAppointmentRequest
        {
            get { return _selectedAppointmentRequest; }
            set { OnPropertyChanged(ref _selectedAppointmentRequest, value); }
        }

        #endregion

        #region Services

        private readonly IDialogService _dialogService;

        private readonly IAppointmentRequestService<AppointmentRequest> _appointmentRequestService;

        private readonly IAppointmentDeleteRequestService _appointmentDeleteRequestService;

        private readonly IAppointmentUpdateRequestService _appointmentUpdateRequestService;

        private readonly IAppointmentService _appointmentService;

        #endregion

        #region Commands

        public ICommand SearchCommand { get; private set; }
        public ICommand MoreInfo { get; private set; }
        public ICommand Accept { get; private set; }
        public ICommand Reject { get; private set; }

        #endregion

        public SecretaryAppointmentRequestsViewModel(IDialogService dialogService, IAppointmentRequestService<AppointmentRequest> appointmentRequestService, IAppointmentDeleteRequestService appointmentDeleteRequestService,
            IAppointmentUpdateRequestService appointmentUpdateRequestService, IAppointmentService appointmentService)
        {
            _dialogService = dialogService;
            _appointmentRequestService = appointmentRequestService;
            _appointmentDeleteRequestService = appointmentDeleteRequestService;
            _appointmentUpdateRequestService = appointmentUpdateRequestService;
            _appointmentService = appointmentService;

            AppointmentRequests = new ObservableCollection<AppointmentRequest>(_appointmentRequestService.ReadAllPendingRequests());

            SearchCommand = new RelayCommand(() =>
            {
                Search();
            });

            MoreInfo = new RelayCommand(() =>
            {
                if (_selectedAppointmentRequest == null)
                {
                    MessageBox.Show("You did not select any appointment request.");
                }
                else
                {
                    if (_selectedAppointmentRequest.ActivityType == ActivityType.Delete)
                    {
                        AppointmentDeleteRequestViewModel appointmentDeleteRequestViewModel = new AppointmentDeleteRequestViewModel(_appointmentDeleteRequestService, _selectedAppointmentRequest.Id);
                        _dialogService.OpenDialog(appointmentDeleteRequestViewModel);
                    }
                    else
                    {
                        AppointmentUpdateRequestViewModel appointmentUpdateRequestViewModel = new AppointmentUpdateRequestViewModel(_appointmentUpdateRequestService, _selectedAppointmentRequest.Id);
                        _dialogService.OpenDialog(appointmentUpdateRequestViewModel);
                    }
                }
            });

            Accept = new RelayCommand(() =>
            {
                if (_selectedAppointmentRequest == null)
                {
                    MessageBox.Show("You did not select any appointment request.");
                }
                else
                {
                    if (_selectedAppointmentRequest.ActivityType == ActivityType.Delete)
                    {
                        var appointmentToDelete = _selectedAppointmentRequest.Appointment;
                        _appointmentService.Delete(appointmentToDelete.Id);
                    }
                    else
                    {
                        var appointmentUpdateRequest = (AppointmentUpdateRequest)_selectedAppointmentRequest;
                        var appointmentToUpdate = _selectedAppointmentRequest.Appointment;
                        appointmentToUpdate.StartDate = appointmentUpdateRequest.StartDate;
                        appointmentToUpdate.EndDate = appointmentUpdateRequest.EndDate;
                        appointmentToUpdate.Doctor = appointmentUpdateRequest.Doctor;
                        appointmentToUpdate.Room = appointmentUpdateRequest.Room;
                        _appointmentService.Update(appointmentToUpdate);
                    }
                    _selectedAppointmentRequest.Status = Status.Approved;
                    _appointmentRequestService.Update(_selectedAppointmentRequest);
                    MessageBox.Show("Request accepted succesfully.");
                    UpdatePage();
                }
            });

            Reject = new RelayCommand(() =>
            {
                if (_selectedAppointmentRequest == null)
                {
                    MessageBox.Show("You did not select any appointment request.");
                }
                else
                {
                    _selectedAppointmentRequest.Status = Status.Rejected;
                    _appointmentRequestService.Update(_selectedAppointmentRequest);
                    UpdatePage();
                    MessageBox.Show("Request rejected succesfully.");
                }
            });

        }

        private void UpdatePage()
        {
            AppointmentRequests = new ObservableCollection<AppointmentRequest>(_appointmentRequestService.ReadAllPendingRequests());
        }

        private void Search()
        {
            if (SearchText == "" || SearchText == null)
                UpdatePage();
            else
                AppointmentRequests = new ObservableCollection<AppointmentRequest>(_appointmentRequestService.FilterPendingRequestsBySearchText(SearchText));
        }
    }
}
