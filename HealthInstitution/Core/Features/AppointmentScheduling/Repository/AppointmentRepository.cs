using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
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
