using HealthInstitution.Model;
using System;

namespace HealthInstitution.Services.Intefaces
{
    public interface IAppointmentUpdateRequestService : IAppointmentRequestService<AppointmentUpdateRequest>
    {
        public bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate);
        public bool IsRoomAvailable(Room room, DateTime fromDate, DateTime toDate);
    }
}
