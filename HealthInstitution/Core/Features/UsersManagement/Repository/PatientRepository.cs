using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Persistence;

namespace HealthInstitution.Core.Features.UsersManagement.Repository
{
    public class PatientRepository : UserRepository<Patient>, IPatientRepository
    {
        public PatientRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
