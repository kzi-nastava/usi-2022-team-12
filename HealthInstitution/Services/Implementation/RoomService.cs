using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;

namespace HealthInstitution.Services.Implementation
{
    public class RoomService : CrudService<Room>, IRoomService
    {
        public RoomService(DatabaseContext context) : base(context)
        {}
    }
}
