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
            base(context)
        { }


        public IEnumerable<Patient> ReadAllValidPatients()
        {
            return _entities.Where(p => p.IsBlocked == false).ToList();
        }

        public IEnumerable<Patient> ReadAllBlockedPatients()
        {
            return _entities.Where(p => p.IsBlocked == true).ToList();
        }

        public IEnumerable<Patient> FilterValidPatientsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _entities.Where(p => p.IsBlocked == false)
                            .Where(p => p.EmailAddress.ToLower().StartsWith(searchText) || p.FirstName.ToLower().StartsWith(searchText)
            || p.LastName.ToLower().StartsWith(searchText) || p.DateOfBirth.ToString().StartsWith(searchText)).ToList();
        }

        public IEnumerable<Patient> FilterBlockedPatientsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _entities.Where(p => p.IsBlocked == true)
                            .Where(p => p.EmailAddress.ToLower().StartsWith(searchText) || p.FirstName.ToLower().StartsWith(searchText)
            || p.LastName.ToLower().StartsWith(searchText) || p.DateOfBirth.ToString().StartsWith(searchText)).ToList();
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
