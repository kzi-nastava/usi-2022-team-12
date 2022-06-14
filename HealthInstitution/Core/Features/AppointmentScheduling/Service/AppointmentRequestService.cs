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
    }
}
