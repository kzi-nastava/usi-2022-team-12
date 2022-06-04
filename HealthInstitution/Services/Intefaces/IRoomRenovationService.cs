using HealthInstitution.Model;
using System;
using System.Collections.Generic;
using HealthInstitution.Model.room;

namespace HealthInstitution.Services.Intefaces
{
    public interface IRoomRenovationService : ICrudService<RoomRenovation>
    {
        public bool IsRoomNotRenovating(Room room, DateTime fromDate, DateTime toDate);
        public bool IsRoomRenovatingMerge(Room room, DateTime fromDate, DateTime toDate);

    }
}
