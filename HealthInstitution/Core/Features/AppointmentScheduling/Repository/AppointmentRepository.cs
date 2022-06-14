using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Repository
{
    public class AppointmentRepository : CrudRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
