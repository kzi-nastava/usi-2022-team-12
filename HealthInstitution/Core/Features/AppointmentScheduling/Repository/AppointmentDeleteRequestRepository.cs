using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;
using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Repository
{
    public class AppointmentDeleteRequestRepository : CrudRepository<AppointmentDeleteRequest>, IAppointmentDeleteRequestRepository
    {
        public AppointmentDeleteRequestRepository(DatabaseContext context) : base(context)
        {

        }

        public IEnumerable<AppointmentDeleteRequest> ReadAllPendingRequests()
        {
            return ReadAll().Where(r => r.Status == Status.Pending);
        }
    }
}
