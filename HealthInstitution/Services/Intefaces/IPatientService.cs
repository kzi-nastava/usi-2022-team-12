using HealthInstitution.Model;
using System.Collections.Generic;

namespace HealthInstitution.Services.Intefaces
{
    public interface IPatientService : IUserService<Patient>
    {
        public IEnumerable<Patient> ReadAllValidPatients();

        public IEnumerable<Patient> ReadAllBlockedPatients();

        public IEnumerable<Patient> FilterPatientsBySearchText(string searchText);

        public IEnumerable<Patient> FilterValidPatientsBySearchText(string searchText);

        public IEnumerable<Patient> FilterBlockedPatientsBySearchText(string searchText);

        public void BlockPatient(Patient patientToBlock);

        public void UnblockPatient(Patient patientToUnblock);
    }


}
