using HealthInstitution.Core.Features.NotificationManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthInstitution.Core.Features.NotificationManagement.Repository
{
    public class UserNotificationRepository : CrudRepository<UserNotification>, IUserNotificationRepository
    {
        public UserNotificationRepository(DatabaseContext context) : base(context)
        {

        }

        public IList<UserNotification> GetValidNotificationsForUser(Guid userId)
        {
            return _entities.Where(n => n.IsShown == false)
                            .Where(n => n.UserId == userId)
                            .ToList();
        }

        public void CreateNotification(Guid userId, string description)
        {
            var newNotification = new UserNotification()
            {
                UserId = userId,
                Content = description,
                IsShown = false
            };

            Create(newNotification);
        }
    }
}
