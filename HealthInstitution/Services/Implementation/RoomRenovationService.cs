using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System.Collections.Generic;
using System.Linq;

namespace HealthInstitution.Services.Implementation
{
    public class RoomRenovationService : CrudService<RoomRenovation>, IRoomRenovationService
    {
        public RoomRenovationService(DatabaseContext context) : base(context)
        {

        }
    }
}
