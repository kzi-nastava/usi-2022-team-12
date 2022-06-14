using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Utility;
using System;

namespace HealthInstitution.Core.Features.RoomManagement.Repository
{
    public interface IRoomRenovationRepository : ICrudRepository<RoomRenovation>
    {
        bool IsRoomNotRenovating(Room room, DateTime fromDate, DateTime toDate);
        bool IsRoomRenovatingMerge(Room room, DateTime fromDate, DateTime toDate);
    }
}
