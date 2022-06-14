using System.Collections.Generic;
using HealthInstitution.Core.Utility;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Repository
{
    public interface IAppointmentRequestRepository : ICrudRepository<AppointmentRequest>
    {
        public IEnumerable<AppointmentRequest> ReadAllPendingRequests();
    }
}
