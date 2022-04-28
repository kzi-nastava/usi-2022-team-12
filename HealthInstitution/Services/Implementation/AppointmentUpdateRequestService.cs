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
    public class AppointmentUpdateRequestService : CrudService<AppointmentUpdateRequest>, IAppointmentUpdateRequestService
    {
        public AppointmentUpdateRequestService(DatabaseContext context) : base(context)
        {

        }

        public bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate)
        {
            return (_entities.Where(req => req.Doctor == doctor && req.StartDate < toDate && fromDate < req.EndDate).Count() == 0);
        }
    }
}
