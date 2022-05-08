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
    public class DoctorService : CrudService<Doctor>, IDoctorService
    {
        public DoctorService(DatabaseContext context) : base(context)
        {
            
        }

        public IEnumerable<Doctor> ReadDoctorsWithSpecialization(DoctorSpecialization specialization)
        {
            return _entities.Where(doc => doc.Specialization == specialization);
        }
    }
}
