using System.Linq;
using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Services.Interfaces;

namespace HealthInstitution.Core.Services.Implementation
{
    public class MedicalRecordService : CrudService<MedicalRecord>, IMedicalRecordService
    {
        public MedicalRecordService(DatabaseContext context) : base(context) { }

        public MedicalRecord GetMedicalRecordForPatient(Patient patient)
        {
            return _entities.Where(e => e.Patient == patient).FirstOrDefault();
        }
    }
}
