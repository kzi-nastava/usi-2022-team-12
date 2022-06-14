using HealthInstitution.Core.Features.MedicalRecordManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Services.Interfaces;

namespace HealthInstitution.Core.Services.Implementation
{
    public class IllnessService : CrudService<Illness>, IIllnessService 
    {
        public IllnessService(DatabaseContext context) : base(context) { }
    }
}
