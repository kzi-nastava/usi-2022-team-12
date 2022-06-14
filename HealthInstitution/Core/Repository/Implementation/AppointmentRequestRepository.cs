using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository.Implementation
{
    public class AppointmentRequestRepository<T> : CrudRepository<T>, IAppointmentRequestRepository<T> where T : AppointmentRequest
    {
        public AppointmentRequestRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
