using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository.Implementation
{
    public class AppointmentUpdateRequestRepository : AppointmentRequestRepository<AppointmentUpdateRequest>, IAppointmentUpdateRequestRepository
    {
        public AppointmentUpdateRequestRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
