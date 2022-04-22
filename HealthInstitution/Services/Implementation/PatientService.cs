using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;

namespace HealthInstitution.Services.Implementation
{
    public class PatientService : UserService<Patient>, IPatientService
    {
        public PatientService(DatabaseContext context) :
            base(context) {}
    }
}
