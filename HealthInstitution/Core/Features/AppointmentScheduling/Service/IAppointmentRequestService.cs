using System.Collections.Generic;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Service
{
    public interface IAppointmentRequestService : ICrudService<AppointmentRequest>
    {
        public IEnumerable<AppointmentRequest> ReadAllPendingRequests();

        public IEnumerable<AppointmentDeleteRequest> ReadAllPendingDeleteRequests();

        public IEnumerable<AppointmentUpdateRequest> ReadAllPendingUpdateRequests();

        public IEnumerable<AppointmentDeleteRequest> FilterPendingDeleteRequestsBySearchText(string searchText);

        public IEnumerable<AppointmentUpdateRequest> FilterPendingUpdateRequestsBySearchText(string searchText);

        public IEnumerable<AppointmentRequest> FilterPendingRequestsBySearchText(string searchText);
    }
}
