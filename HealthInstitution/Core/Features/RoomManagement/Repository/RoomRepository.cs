using HealthInstitution.Persistence;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.RoomManagement.Repository
{
    public class RoomRepository : CrudRepository<Room>, IRoomRepository
    {

        public RoomRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
