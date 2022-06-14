using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Persistence;

namespace HealthInstitution.Core.Features.UsersManagement.Repository
{
    public class DoctorRepository : UserRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
