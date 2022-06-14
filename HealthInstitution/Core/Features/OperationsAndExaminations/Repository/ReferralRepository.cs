using HealthInstitution.Core.Features.OperationsAndExaminations.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.OperationsAndExaminations.Repository
{
    public class ReferralRepository : CrudRepository<Referral>, IReferralRepository
    {
        public ReferralRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
