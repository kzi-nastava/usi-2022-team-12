using HealthInstitution.Persistence;
using HealthInstitution.Core.Repository.Interfaces;
using HealthInstitution.Core.Features.RoomManagement.Model;

namespace HealthInstitution.Core.Repository.Implementation
{
    public class RoomRenovationRepository : CrudRepository<RoomRenovation>, IRoomRenovationRepository
    {
        public RoomRenovationRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
