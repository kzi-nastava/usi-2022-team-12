using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository.Implementation
{
    public class AppointmentDeleteRequestRepository : AppointmentRequestRepository<AppointmentDeleteRequest>, IAppointmentDeleteRequestRepository
    {
        public AppointmentDeleteRequestRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
