using HealthInstitution.Model;

namespace HealthInstitution.Services.Intefaces
{
    public interface IDoctorService : IUserService<Doctor>
    {
        public IEnumerable<Doctor> FindDoctorsWithSpecialization(DoctorSpecialization specialization);
    }
}
