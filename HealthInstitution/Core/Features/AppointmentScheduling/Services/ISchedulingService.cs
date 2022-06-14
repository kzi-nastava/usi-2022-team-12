using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Services
{
    public interface ISchedulingService
    {
        Room FindFreeRoom(RoomType roomType, DateTime start, DateTime end);
        IEnumerable<Appointment> GetAppointmentsForDateRangeAndDoctor(DateTime start, DateTime end, Doctor doctor);
        bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate);
        bool IsDoctorAvailableForUpdate(Doctor doctor, DateTime fromDate, DateTime toDate, Appointment aptToUpdate);
        bool IsRoomAvailable(Room room, DateTime fromDate, DateTime toDate);
        bool IsRoomAvailableForUpdate(Room room, DateTime fromDate, DateTime toDate, Appointment aptToUpdate);
        IEnumerable<Appointment> ReadFinishedAppointmentsForPatient(Patient pt);
        IEnumerable<Appointment> ReadFuturePatientAppointments(Patient pt);
        IEnumerable<Appointment> ReadPatientAppointments(Patient pt);
        IEnumerable<Appointment> ReadRoomAppointments(Room r);
    }
}
