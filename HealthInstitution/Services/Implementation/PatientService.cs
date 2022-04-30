using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System.Collections.Generic;
using System.Linq;

namespace HealthInstitution.Services.Implementation
{
    public class PatientService : UserService<Patient>, IPatientService
    {
        public PatientService(DatabaseContext context) :
            base(context) {}


        public IEnumerable<Patient> ReadAllValidPatients()
        {
            return _entities.Where(p => p.IsBlocked == false).ToList();
        }

        public IEnumerable<Patient> ReadAllBlockedPatients()
        {
            return _entities.Where(p => p.IsBlocked == true).ToList();
        }

        public IEnumerable<Patient> FilterPatientsBySearchText(string searchText)
        {
            string searchTextLower = searchText.ToLower();
            return _entities.Where(p => p.EmailAddress.ToLower().StartsWith(searchTextLower) || p.FirstName.ToLower().StartsWith(searchTextLower) 
            || p.LastName.ToLower().StartsWith(searchTextLower) || p.DateOfBirth.ToString().ToLower().StartsWith(searchTextLower)).ToList();
        }

        public void BlockPatient(Patient patientToBlock)
        {
            patientToBlock.IsBlocked = true;
            Update(patientToBlock);
        }

        public void UnblockPatient(Patient patientToUnblock)
        {
            patientToUnblock.IsBlocked = false;
            Update(patientToUnblock);
        }
    }
}
