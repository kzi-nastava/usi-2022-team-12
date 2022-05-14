using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthInstitution.Services.Implementation
{
    public class NotificationService : CrudService<Notification>, INotificationService
    {
        public NotificationService(DatabaseContext context) : base(context)
        {
        }

        public IList<Notification> GetValidNotificationsForUser(Guid userId)
        {
            return _entities.Where(n => n.IsShown == false)
                            .Where(n => n.Id == userId)
                            .ToList();
        }
    }
}
