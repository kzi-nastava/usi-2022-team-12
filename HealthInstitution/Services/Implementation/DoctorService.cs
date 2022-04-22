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
    public class DoctorService<T> : CrudService<T>, IDoctorService<T> where T : Doctor
    {
        public DoctorService(DatabaseContext context) : base(context)
        {

        }
    }
}
