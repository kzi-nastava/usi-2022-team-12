using System;
using System.Collections.Generic;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Repository
{
    public interface IAppointmentUpdateRequestRepository : ICrudRepository<AppointmentUpdateRequest>
    {
        public IEnumerable<AppointmentUpdateRequest> ReadAllPendingRequests();

        public bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate);

        public bool IsRoomAvailable(Room room, DateTime fromDate, DateTime toDate);
    }
}
