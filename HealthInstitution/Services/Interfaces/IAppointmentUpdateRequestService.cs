using System;
using HealthInstitution.Model.patient;
using HealthInstitution.Model.room;
using HealthInstitution.Model.user;

namespace HealthInstitution.Services.Interfaces
{
    public interface IAppointmentUpdateRequestService : IAppointmentRequestService<AppointmentUpdateRequest>
    {
        public bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate);

        public bool IsRoomAvailable(Room room, DateTime fromDate, DateTime toDate);
    }
}
