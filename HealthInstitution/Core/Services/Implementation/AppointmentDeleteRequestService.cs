using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.Services.Implementation
{
    public class AppointmentDeleteRequestService : AppointmentRequestService<AppointmentDeleteRequest>, IAppointmentDeleteRequestService
    {
        public AppointmentDeleteRequestService(DatabaseContext context) : base(context)
        {

        }
    }
}
