using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Service
{
    public class AppointmentRequestService : IAppointmentRequestService
    {
        private readonly IAppointmentRequestRepository _appointmentRequestRepository;

        private readonly IAppointmentDeleteRequestRepository _appointmentDeleteRequestRepository;

        private readonly IAppointmentUpdateRequestRepository _appointmentUpdateRequestRepository;

        public AppointmentRequestService(DatabaseContext context,
            IAppointmentRequestRepository appointmentRequestRepository,
            IAppointmentDeleteRequestRepository appointmentDeleteRequestRepository,
            IAppointmentUpdateRequestRepository appointmentUpdateRequestRepository)
        {
            _appointmentRequestRepository = appointmentRequestRepository;
            _appointmentDeleteRequestRepository = appointmentDeleteRequestRepository;
            _appointmentUpdateRequestRepository = appointmentUpdateRequestRepository;
        }

        #region CRUD methods

        public IEnumerable<AppointmentRequest> ReadAll()
        {
            return _appointmentRequestRepository.ReadAll();
        }

        public AppointmentRequest Read(Guid requestId)
        {
            return _appointmentRequestRepository.Read(requestId);
        }

        public AppointmentRequest Create(AppointmentRequest newRequest)
        {
            return _appointmentRequestRepository.Create(newRequest);
        }

        public AppointmentRequest Update(AppointmentRequest requestToUpdate)
        {
            return _appointmentRequestRepository.Update(requestToUpdate);
        }

        public AppointmentRequest Delete(Guid requestId)
        {
            return _appointmentRequestRepository.Delete(requestId);
        }

        #endregion

        public IEnumerable<AppointmentRequest> ReadAllPendingRequests()
        {
            return _appointmentRequestRepository.ReadAllPendingRequests();
        }

        public IEnumerable<AppointmentDeleteRequest> ReadAllPendingDeleteRequests()
        {
            return _appointmentDeleteRequestRepository.ReadAllPendingRequests();
        }

        public IEnumerable<AppointmentUpdateRequest> ReadAllPendingUpdateRequests()
        {
            return _appointmentUpdateRequestRepository.ReadAllPendingRequests();
        }

        public IEnumerable<AppointmentDeleteRequest> FilterPendingDeleteRequestsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _appointmentDeleteRequestRepository.ReadAll()
                                                      .Where(r => r.Status == Status.Pending)
                            .Where(r => r.Patient.EmailAddress.ToLower().Contains(searchText) || r.CreatedAt.ToString().Contains(searchText)
                            || r.Patient.FirstName.ToLower().Contains(searchText)).ToList();
        }

        public IEnumerable<AppointmentUpdateRequest> FilterPendingUpdateRequestsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _appointmentUpdateRequestRepository.ReadAll().Where(r => r.Status == Status.Pending)
                .Where(r => r.Patient.EmailAddress.ToLower().Contains(searchText) || r.CreatedAt.ToString().Contains(searchText)
                    || r.Patient.FirstName.ToLower().Contains(searchText)).ToList();
        }

        public IEnumerable<AppointmentRequest> FilterPendingRequestsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _appointmentRequestRepository.ReadAll().Where(r => r.Status == Status.Pending)
                .Where(r => r.Patient.EmailAddress.ToLower().Contains(searchText) || r.CreatedAt.ToString().Contains(searchText)
                    || r.Patient.FirstName.ToLower().Contains(searchText)).ToList();
        }
    }
}
