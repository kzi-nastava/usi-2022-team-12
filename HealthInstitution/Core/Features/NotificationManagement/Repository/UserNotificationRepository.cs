using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.NotificationManagement.Repository
{
    public class UserNotificationRepository : CrudRepository<Notification>, IUserNotificationRepository
    {
        public UserNotificationRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
