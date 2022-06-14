using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;
using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Repository
{
    public class AppointmentUpdateRequestRepository : CrudRepository<AppointmentUpdateRequest>, IAppointmentUpdateRequestRepository
    {
        public AppointmentUpdateRequestRepository(DatabaseContext context) : base(context)
        {

        }

        public IEnumerable<AppointmentUpdateRequest> ReadAllPendingRequests()
        {
            return ReadAll().Where(r => r.Status == Status.Pending);
        }
    }
}
