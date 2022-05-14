using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;

namespace HealthInstitution.Services.Implementation
{
    public class DoctorService : UserService<Doctor>, IDoctorService
    {
        public DoctorService(DatabaseContext context) : base(context)
        {
            
        }

        public IEnumerable<Doctor> FindDoctorsWithSpecialization(DoctorSpecialization specialization)
        {
            return _entities.Where(doc => doc.Specialization == specialization);
        }
    }
}
