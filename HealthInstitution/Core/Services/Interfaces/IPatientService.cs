using System.Collections.Generic;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Services.Interfaces
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
