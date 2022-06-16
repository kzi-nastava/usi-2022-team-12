using HealthInstitution.Core.Utility;
using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Features.MedicalRecordManagement.Repository
{
    public interface IMedicalRecordRepository : ICrudRepository<MedicalRecord>
    {
        MedicalRecord GetMedicalRecordForPatient(Patient patient);
    }
}
