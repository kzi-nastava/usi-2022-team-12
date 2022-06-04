using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System;
using System.Linq;
using HealthInstitution.Model.patient;
using HealthInstitution.Model.room;
using HealthInstitution.Model.user;

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
