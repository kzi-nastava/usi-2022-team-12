using System;
using System.Collections.Generic;
using HealthInstitution.Model;

namespace HealthInstitution.Services.Interfaces
{
    public interface INotificationService : ICrudService<Notification>
    {
        public IList<Notification> GetValidNotificationsForUser(Guid userId);

        public void CreateNotification(Guid userId, string description);
    }
}
