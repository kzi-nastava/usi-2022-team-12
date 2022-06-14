using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.MedicalRecordManagement.Repository
{
    public class IllnessRepository : CrudRepository<Illness>, IIllnessRepository
    {
        public IllnessRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
}
