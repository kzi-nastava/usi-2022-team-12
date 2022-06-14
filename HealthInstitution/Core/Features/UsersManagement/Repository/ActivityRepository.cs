using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.UsersManagement.Repository
{
    public class ActivityRepository : CrudRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(DatabaseContext context) : base(context)
        {
        }

        public IEnumerable<Activity> ReadPatientUpdateOrRemoveActivity(Patient pt, int interval)
        {
            return _entities.Where(act => act.Patient == pt && act.DateOfAction > DateTime.Now.AddDays(-interval) && act.ActivityType != ActivityType.Create);
        }

        public IEnumerable<Activity> ReadPatientMakeActivity(Patient pt, int interval)
        {
            return _entities.Where(act => act.Patient == pt && act.DateOfAction > DateTime.Now.AddDays(-interval) && act.ActivityType == ActivityType.Create);
        }
    }
}
