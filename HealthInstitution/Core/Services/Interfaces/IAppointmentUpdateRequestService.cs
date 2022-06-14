using System;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Services;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Services.Interfaces
{
    public interface IAppointmentUpdateRequestService : IAppointmentRequestService<AppointmentUpdateRequest>
    {
        public bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate);

        public bool IsRoomAvailable(Room room, DateTime fromDate, DateTime toDate);
    }
}
