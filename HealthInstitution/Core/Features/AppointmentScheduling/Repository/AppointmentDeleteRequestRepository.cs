using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Repository
{
    public class AppointmentDeleteRequestRepository : AppointmentRequestRepository<AppointmentDeleteRequest>, IAppointmentDeleteRequestRepository
    {
        public AppointmentDeleteRequestRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
