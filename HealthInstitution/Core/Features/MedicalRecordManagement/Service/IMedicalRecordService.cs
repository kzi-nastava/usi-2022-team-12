using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Features.MedicalRecordManagement.Service
{
    public interface IMedicalRecordService
    {
        MedicalRecord GetMedicalRecordForPatient(Patient patient);
    }
}