using HealthInstitution.Core.Features.OffDaysManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.OffDaysManagement.Repository
{
    public class OffDaysRequestRepository : CrudRepository<OffDaysRequest>, IOffDaysRequestRepository
    {
        public OffDaysRequestRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
