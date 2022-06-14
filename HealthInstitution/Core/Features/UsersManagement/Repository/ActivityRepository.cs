using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.UsersManagement.Repository
{
    public class ActivityRepository : CrudRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
