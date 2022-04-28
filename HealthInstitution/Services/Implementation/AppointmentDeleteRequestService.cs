using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services.Implementation
{
    public class AppointmentDeleteRequestService : CrudService<AppointmentDeleteRequest>, IAppointmentDeleteRequestService
    {
        public AppointmentDeleteRequestService(DatabaseContext context) : base(context)
        {

        }
    }
}
