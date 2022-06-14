using HealthInstitution.Model.appointment;
using HealthInstitution.Model.user;

namespace HealthInstitution.Services.Interfaces
{
    public interface IMedicalRecordService : ICrudService<MedicalRecord>
    {
        MedicalRecord GetMedicalRecordForPatient(Patient patient);
    }
}
