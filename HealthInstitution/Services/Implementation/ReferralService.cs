using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;

namespace HealthInstitution.Services.Implementation
{
    public class ReferralService : CrudService<Referral>, IReferralService
    {
        public ReferralService(DatabaseContext context) : base(context)
        {
        }
    }
}
