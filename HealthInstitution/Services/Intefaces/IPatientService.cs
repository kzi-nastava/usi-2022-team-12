using HealthInstitution.Model;
using System.Collections.Generic;

namespace HealthInstitution.Services.Intefaces
{
    public interface IPatientService : IUserService<Patient>
    {
        public IEnumerable<Patient> ReadAllValidPatients();

        public IEnumerable<Patient> ReadAllBlockedPatients();
    }


}
