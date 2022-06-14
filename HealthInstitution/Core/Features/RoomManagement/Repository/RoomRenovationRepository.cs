using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;
using System;
using System.Linq;

namespace HealthInstitution.Core.Features.RoomManagement.Repository
{
    public class RoomRenovationRepository : CrudRepository<RoomRenovation>, IRoomRenovationRepository
    {
        public RoomRenovationRepository(DatabaseContext context) : base(context)
        {

        }
        public bool IsRoomNotRenovating(Room room, DateTime fromDate, DateTime toDate)
        {
            return ((_entities.Where(renovation => renovation.RenovatedRoom == room && renovation.StartTime < toDate && fromDate < renovation.EndTime)).Count() == 0)
                && IsRoomRenovatingMerge(room, fromDate, toDate);
        }

        public bool IsRoomRenovatingMerge(Room room, DateTime fromDate, DateTime toDate)
        {
            return ((_entities.Where(renovation => renovation.AdvancedDivide == false &&
            (renovation.RenovatedSmallRoom1Name == room.Name || renovation.RenovatedSmallRoom2Name == room.Name) &&
            renovation.StartTime < toDate && fromDate < renovation.EndTime)).Count() == 0);
        }
    }
}
