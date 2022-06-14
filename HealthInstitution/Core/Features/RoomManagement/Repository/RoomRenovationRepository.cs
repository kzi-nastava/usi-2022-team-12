using HealthInstitution.Persistence;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.RoomManagement.Repository
{
    public class RoomRenovationRepository : CrudRepository<RoomRenovation>, IRoomRenovationRepository
    {
        public RoomRenovationRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
