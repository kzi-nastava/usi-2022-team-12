using HealthInstitution.Model;
using HealthInstitution.Persistence;
using System;
using System.Linq;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;

namespace HealthInstitution.Services.Implementation
{
    public class AppointmentUpdateRequestService : AppointmentRequestService<AppointmentUpdateRequest>, IAppointmentUpdateRequestService
    {
        public AppointmentUpdateRequestService(DatabaseContext context) : base(context)
        {

        }

        public bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate)
        {
            return (_entities.Where(req => req.Doctor == doctor && req.StartDate < toDate && fromDate < req.EndDate).Count() == 0);
        }

        public bool IsRoomAvailable(Room room, DateTime fromDate, DateTime toDate)
        {
            return (_entities.Where(req => req.Room == room && req.StartDate < toDate && fromDate < req.EndDate).Count() == 0);
        }
    }
}
