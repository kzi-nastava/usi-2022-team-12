using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Services.Interfaces
{
    public interface IMedicalRecordService : ICrudService<MedicalRecord>
    {
        MedicalRecord GetMedicalRecordForPatient(Patient patient);
    }
}
