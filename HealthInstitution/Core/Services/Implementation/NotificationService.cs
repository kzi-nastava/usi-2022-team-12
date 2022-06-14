using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Services.Interfaces;

namespace HealthInstitution.Core.Services.Implementation
{
    public class NotificationService : CrudService<Notification>, INotificationService
    {
        public NotificationService(DatabaseContext context) : base(context)
        {
        }

        public IList<Notification> GetValidNotificationsForUser(Guid userId)
        {
            return _entities.Where(n => n.IsShown == false)
                            .Where(n => n.UserId == userId)
                            .ToList();
        }

        public void CreateNotification(Guid userId, string description)
        {
            var newNotification = new Notification()
            {
                UserId = userId,
                Content = description,
                IsShown = false
            };

            Create(newNotification);
        }
    }
}
