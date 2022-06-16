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
    }
}
