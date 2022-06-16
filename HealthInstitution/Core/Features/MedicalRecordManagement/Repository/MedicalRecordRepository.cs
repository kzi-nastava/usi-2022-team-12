using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;
using System.Linq;

namespace HealthInstitution.Core.Features.MedicalRecordManagement.Repository
{
    public class MedicalRecordRepository : CrudRepository<MedicalRecord>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(DatabaseContext context) : base(context)
        {

        }
        public MedicalRecord GetMedicalRecordForPatient(Patient patient)
        {
            return _entities.Where(e => e.Patient.Id == patient.Id).FirstOrDefault();
        }
    }
}
