using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.AppointmentScheduling.Service;
using HealthInstitution.Core.Features.AppointmentScheduling.Services;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Services.Interfaces;
using HealthInstitution.Core.Utility.Command;
using HealthInstitution.Core.Utility.HelperClasses;
using HealthInstitution.GUI.Features.AppointmentScheduling.Dialog;
using HealthInstitution.GUI.Utility.Dialog.Service;

namespace HealthInstitution.GUI.Features.AppointmentScheduling
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

        private readonly IAppointmentRequestService _appointmentRequestService;

        private readonly IAppointmentRequestRepository _appointmentRequestRepository;

        private readonly IAppointmentDeleteRequestRepository _appointmentDeleteRequestService;

        private readonly IAppointmentUpdateRequestRepository _appointmentUpdateRequestService;

        private readonly IAppointmentRepository _appointmentRepository;

        #endregion

        #region Commands

        public ICommand SearchCommand { get; private set; }

        public ICommand MoreInfo { get; private set; }

        public ICommand Accept { get; private set; }

        public ICommand Reject { get; private set; }

        #endregion

        public SecretaryAppointmentRequestsViewModel(IDialogService dialogService, IAppointmentRequestService appointmentRequestService, IAppointmentDeleteRequestRepository appointmentDeleteRequestRepository,
            IAppointmentUpdateRequestRepository appointmentUpdateRequestRepository, IAppointmentRepository appointmentRepository, IAppointmentRequestRepository appointmentRequestRepository)
        {
            _dialogService = dialogService;
            _appointmentRequestService = appointmentRequestService;
            _appointmentDeleteRequestService = appointmentDeleteRequestRepository;
            _appointmentUpdateRequestService = appointmentUpdateRequestRepository;
            _appointmentRepository = appointmentRepository;
            _appointmentRequestRepository = appointmentRequestRepository;

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
                        AppointmentDeleteRequestViewModel appointmentDeleteRequestViewModel = new AppointmentDeleteRequestViewModel(appointmentDeleteRequestRepository, _selectedAppointmentRequest.Id);
                        _dialogService.OpenDialog(appointmentDeleteRequestViewModel);
                    }
                    else
                    {
                        AppointmentUpdateRequestViewModel appointmentUpdateRequestViewModel = new AppointmentUpdateRequestViewModel(appointmentUpdateRequestRepository, _selectedAppointmentRequest.Id);
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
                        _appointmentRepository.Delete(appointmentToDelete.Id);
                    }
                    else
                    {
                        var appointmentUpdateRequest = (AppointmentUpdateRequest)_selectedAppointmentRequest;
                        var appointmentToUpdate = _selectedAppointmentRequest.Appointment;
                        appointmentToUpdate.StartDate = appointmentUpdateRequest.StartDate;
                        appointmentToUpdate.EndDate = appointmentUpdateRequest.EndDate;
                        appointmentToUpdate.Doctor = appointmentUpdateRequest.Doctor;
                        appointmentToUpdate.Room = appointmentUpdateRequest.Room;
                        _appointmentRepository.Update(appointmentToUpdate);
                    }
                    _selectedAppointmentRequest.Status = Status.Approved;
                    _appointmentRequestRepository.Update(_selectedAppointmentRequest);
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
                    _appointmentRequestRepository.Update(_selectedAppointmentRequest);
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
            if (SearchText is "" or null)
                UpdatePage();
            else
                AppointmentRequests = new ObservableCollection<AppointmentRequest>(_appointmentRequestService.FilterPendingRequestsBySearchText(SearchText));
        }
    }
}
