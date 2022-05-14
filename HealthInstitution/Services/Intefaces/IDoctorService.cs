using HealthInstitution.Model;
using System.Collections.Generic;

namespace HealthInstitution.Services.Intefaces
{
    public interface IDoctorService : IUserService<Doctor>
    {
        public IEnumerable<Doctor> FindDoctorsWithSpecialization(DoctorSpecialization specialization);
    }
}
