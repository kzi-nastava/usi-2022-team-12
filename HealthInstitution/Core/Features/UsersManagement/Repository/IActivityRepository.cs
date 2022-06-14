using System.Collections.Generic;
using HealthInstitution.Core.Utility;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Features.UsersManagement.Repository
{
    public interface IActivityRepository : ICrudRepository<Activity>
    {
        public IEnumerable<Activity> ReadPatientUpdateOrRemoveActivity(Patient pt, int interval);

        public IEnumerable<Activity> ReadPatientMakeActivity(Patient pt, int interval);
    }
}
