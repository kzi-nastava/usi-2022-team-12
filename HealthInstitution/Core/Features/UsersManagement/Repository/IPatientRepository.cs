using System.Collections.Generic;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Features.UsersManagement.Repository
{
    public interface IPatientRepository : IUserRepository<Patient>
    {
        public IEnumerable<Patient> ReadAllValidPatients();

        public IEnumerable<Patient> ReadAllBlockedPatients();

        public IEnumerable<Patient> FilterPatientsBySearchText(string searchText);

        public IEnumerable<Patient> FilterValidPatientsBySearchText(string searchText);

        public IEnumerable<Patient> FilterBlockedPatientsBySearchText(string searchText);
    }
}
