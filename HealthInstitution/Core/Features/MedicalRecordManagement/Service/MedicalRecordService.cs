using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Features.MedicalRecordManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Features.MedicalRecordManagement.Service
{
    public class MedicalRecordService : IMedicalRecordService
    {
        IMedicalRecordRepository _medicalRecordRepository;

        public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public MedicalRecord GetMedicalRecordForPatient(Patient patient)
        {
            return _medicalRecordRepository.GetMedicalRecordForPatient(patient);
        }
    }
}
