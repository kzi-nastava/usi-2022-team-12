using HealthInstitution.Core.Features.NotificationManagement.Model;
using HealthInstitution.Core.Utility;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Features.NotificationManagement.Service
{
    public interface IUserNotificationService : ICrudService<UserNotification>
    {
        public IList<UserNotification> GetValidNotificationsForUser(Guid userId);

        public void CreateNotification(Guid userId, string description);
    }
}
