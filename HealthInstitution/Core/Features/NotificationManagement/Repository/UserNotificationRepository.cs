using HealthInstitution.Core.Features.NotificationManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.NotificationManagement.Repository
{
    public class UserNotificationRepository : CrudRepository<UserNotification>, IUserNotificationRepository
    {
        public UserNotificationRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
