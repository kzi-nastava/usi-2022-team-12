using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System.Collections.Generic;
using System.Linq;

namespace HealthInstitution.Services.Implementation
{
    public class RoomService : CrudService<Room>, IRoomService
    {
        public RoomService(DatabaseContext context) : base(context)
        {
        
        }

        public IEnumerable<Room> ReadRoomsWithType(RoomType rt) {
            return _entities.Where(rm => rm.RoomType == rt).ToList();
        }
    }
}
