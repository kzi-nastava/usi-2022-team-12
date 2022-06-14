using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository.Implementation
{
    public class NotificationRepository : CrudRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
