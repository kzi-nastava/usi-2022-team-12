using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System.Linq;

namespace HealthInstitution.Services.Implementation
{
    public class PatientService : UserService<Patient>, IPatientService
    {
        public PatientService(DatabaseContext context) :
            base(context) {}
    }
}
