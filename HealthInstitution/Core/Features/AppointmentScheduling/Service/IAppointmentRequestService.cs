using System.Collections.Generic;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Service
{
    public interface IAppointmentRequestService
    {
        public IEnumerable<AppointmentRequest> ReadAllPendingRequests();

        public IEnumerable<AppointmentDeleteRequest> ReadAllPendingDeleteRequests();

        public IEnumerable<AppointmentUpdateRequest> ReadAllPendingUpdateRequests();
        public IEnumerable<AppointmentDeleteRequest> FilterPendingDeleteRequestsBySearchText(string searchText);

        public IEnumerable<AppointmentUpdateRequest> FilterPendingUpdateRequestsBySearchText(string searchText);
    }
}
