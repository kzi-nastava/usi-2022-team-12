using HealthInstitution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services.Intefaces
{
    public interface IAppointmentUpdateRequestService : ICrudService<AppointmentUpdateRequest>
    {
        public bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate);
        public bool IsRoomAvailable(Room room, DateTime fromDate, DateTime toDate);
    }
}
