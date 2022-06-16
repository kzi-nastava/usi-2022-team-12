using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;
using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Repository
{
    public class AppointmentUpdateRequestRepository : CrudRepository<AppointmentUpdateRequest>, IAppointmentUpdateRequestRepository
    {
        public AppointmentUpdateRequestRepository(DatabaseContext context) : base(context)
        {

        }

        public IEnumerable<AppointmentUpdateRequest> ReadAllPendingRequests()
        {
            return ReadAll().Where(r => r.Status == Status.Pending);
        }

        public bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate)
        {
            return (_entities.Where(req => req.Doctor.Id == doctor.Id && req.StartDate < toDate && fromDate < req.EndDate).Count() == 0);
        }

        public bool IsRoomAvailable(Room room, DateTime fromDate, DateTime toDate)
        {
            return (_entities.Where(req => req.Room.Id == room.Id && req.StartDate < toDate && fromDate < req.EndDate).Count() == 0);
        }
    }
}
