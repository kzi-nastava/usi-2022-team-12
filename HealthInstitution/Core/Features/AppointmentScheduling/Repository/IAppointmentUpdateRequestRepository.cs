using System.Collections.Generic;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Repository
{
    public interface IAppointmentUpdateRequestRepository : ICrudRepository<AppointmentUpdateRequest>
    {
        public IEnumerable<AppointmentUpdateRequest> ReadAllPendingRequests();
    }
}
