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
    public class AppointmentService<T> : CrudService<T>, IAppointmentService<T> where T : Appointment
    {
        public AppointmentService(DatabaseContext context) : base(context)
        {

        }
    }
}
