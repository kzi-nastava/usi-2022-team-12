using HealthInstitution.Persistence;
using HealthInstitution.Core.Repository.Interfaces;
using HealthInstitution.Core.Features.RoomManagement.Model;

namespace HealthInstitution.Core.Repository.Implementation
{
    public class RoomRepository : CrudRepository<Room>, IRoomRepository
    {

        public RoomRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
