using HealthInstitution.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Repository
{
    public interface IAppointmentRepository : ICrudRepository<Appointment>
    {
    }
}
