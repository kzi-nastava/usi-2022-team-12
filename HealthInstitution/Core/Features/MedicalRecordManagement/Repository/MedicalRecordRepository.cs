using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.MedicalRecordManagement.Repository
{
    public class MedicalRecordRepository : CrudRepository<MedicalRecord>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
