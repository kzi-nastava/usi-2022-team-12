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

        public IEnumerable<Patient> FilterPatientsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _entities.Where(p => p.EmailAddress.ToLower().Contains(searchText) || p.FirstName.ToLower().Contains(searchText)
           || p.LastName.ToLower().Contains(searchText) || p.DateOfBirth.ToString().Contains(searchText)).ToList();
        }

        public IEnumerable<Patient> FilterValidPatientsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _entities.Where(p => p.IsBlocked == false)
                            .Where(p => p.EmailAddress.ToLower().Contains(searchText) || p.FirstName.ToLower().Contains(searchText)
            || p.LastName.ToLower().Contains(searchText) || p.DateOfBirth.ToString().Contains(searchText)).ToList();
        }

        public IEnumerable<Patient> FilterBlockedPatientsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _entities.Where(p => p.IsBlocked == true)
                            .Where(p => p.EmailAddress.ToLower().Contains(searchText) || p.FirstName.ToLower().Contains(searchText)
            || p.LastName.ToLower().Contains(searchText) || p.DateOfBirth.ToString().Contains(searchText)).ToList();
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
