using HealthInstitution.Core.Utility;
using HealthInstitution.Core.Features.NotificationManagement.Model;
using System.Collections.Generic;
using System;

namespace HealthInstitution.Core.Features.NotificationManagement.Repository
{
    public interface IUserNotificationRepository : ICrudRepository<UserNotification>
    {
        void CreateNotification(Guid userId, string description);
        IList<UserNotification> GetValidNotificationsForUser(Guid userId);
    }
}
