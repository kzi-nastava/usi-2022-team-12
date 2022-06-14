using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Service
{
    public interface ISchedulingService
    {
        IEnumerable<Appointment> GetAppointmentsForDateRangeAndDoctor(DateTime start, DateTime end, Doctor doctor);
        IEnumerable<Appointment> ReadFinishedAppointmentsForPatient(Patient pt);
        IEnumerable<Appointment> ReadFuturePatientAppointments(Patient pt);
        IEnumerable<Appointment> ReadPatientAppointments(Patient pt);
        IEnumerable<Appointment> ReadRoomAppointments(Room r);
    }
}
