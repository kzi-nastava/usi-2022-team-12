using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Repository
{
    public class AppointmentRepository : CrudRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
