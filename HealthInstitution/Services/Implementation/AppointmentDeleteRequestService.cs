using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;

namespace HealthInstitution.Services.Implementation
{
    public class AppointmentDeleteRequestService : AppointmentRequestService<AppointmentDeleteRequest>, IAppointmentDeleteRequestService
    {
        public AppointmentDeleteRequestService(DatabaseContext context) : base(context)
        {

        }
    }
}
