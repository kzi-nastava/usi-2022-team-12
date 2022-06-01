using HealthInstitution.Model;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Services.Intefaces
{
    public interface INotificationService : ICrudService<Notification>
    {
        public IList<Notification> GetValidNotificationsForUser(Guid userId);

    }
}
