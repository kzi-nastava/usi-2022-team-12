using System.Collections.Generic;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Persistence;
using System.Linq;

namespace HealthInstitution.Core.Features.UsersManagement.Repository
{
    public class PatientRepository : UserRepository<Patient>, IPatientRepository
    {
        public PatientRepository(DatabaseContext context) : base(context)
        {

        }

        public IEnumerable<Patient> ReadAllValidPatients()
        {
            return _entities.Where(p => p.IsBlocked == false).ToList();
        }

        public IEnumerable<Patient> ReadAllBlockedPatients()
        {
            return _entities.Where(p => p.IsBlocked).ToList();
        }

        public IEnumerable<Patient> FilterPatientsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _entities.Where(p => p.EmailAddress.ToLower().Contains(searchText) ||
                                        p.FirstName.ToLower().Contains(searchText) ||
                                        p.LastName.ToLower().Contains(searchText) ||
                                        p.DateOfBirth.ToString().Contains(searchText)).ToList();
        }

        public IEnumerable<Patient> FilterValidPatientsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _entities.Where(p => p.IsBlocked == false)
                            .Where(p => p.EmailAddress.ToLower().Contains(searchText) ||
                                        p.FirstName.ToLower().Contains(searchText) ||
                                        p.LastName.ToLower().Contains(searchText) ||
                                        p.DateOfBirth.ToString().Contains(searchText)).ToList();
        }

        public IEnumerable<Patient> FilterBlockedPatientsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _entities.Where(p => p.IsBlocked == true)
                            .Where(p => p.EmailAddress.ToLower().Contains(searchText) ||
                                        p.FirstName.ToLower().Contains(searchText) ||
                                        p.LastName.ToLower().Contains(searchText) ||
                                        p.DateOfBirth.ToString().Contains(searchText)).ToList();
        }
    }
}
