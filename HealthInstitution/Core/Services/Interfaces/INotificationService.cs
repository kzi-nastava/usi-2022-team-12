using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Services.Interfaces
{
    public interface INotificationService : ICrudService<Notification>
    {
        public IList<Notification> GetValidNotificationsForUser(Guid userId);

        public void CreateNotification(Guid userId, string description);
    }
}
