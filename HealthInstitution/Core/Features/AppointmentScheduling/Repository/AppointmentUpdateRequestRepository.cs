using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Repository
{
    public class AppointmentUpdateRequestRepository : AppointmentRequestRepository<AppointmentUpdateRequest>, IAppointmentUpdateRequestRepository
    {
        public AppointmentUpdateRequestRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
