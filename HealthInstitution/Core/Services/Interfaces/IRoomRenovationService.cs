using System;
using HealthInstitution.Core.Features.RoomManagement.Model;

namespace HealthInstitution.Services.Interfaces
{
    public interface IRoomRenovationService : ICrudService<RoomRenovation>
    {
        public bool IsRoomNotRenovating(Room room, DateTime fromDate, DateTime toDate);
        public bool IsRoomRenovatingMerge(Room room, DateTime fromDate, DateTime toDate);

    }
}
