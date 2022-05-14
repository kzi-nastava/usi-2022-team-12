using HealthInstitution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services.Intefaces
{
    public interface IAppointmentService : ICrudService<Appointment>
    {
        public IEnumerable<Appointment> ReadFinishedAppointmentsForPatient(Patient pt);
        public IEnumerable<Appointment> ReadPatientAppointments(Patient pt);
        public IEnumerable<Appointment> ReadFuturePatientAppointments(Patient pt);
        public IEnumerable<Appointment> ReadRoomAppointments(Room r);
        public IEnumerable<Appointment> FindFinishedAppointmentsWithAnamnesis(Patient patient, string text);
        public IEnumerable<Appointment> GetAppointmentsForDateRangeAndDoctor(DateTime start, DateTime end, Doctor doctor);
        public Appointment FindFirstFreeAppointmentForDoctor(Patient patient, Doctor doctor, DateTime deadline);
        public Appointment FindFirstFreeAppointmentForInterval(Patient patient, DoctorSpecialization specialization, DateTime startIntervalBound, DateTime endIntervalBound, DateTime deadline);
        public Appointment FindFirstFreeAppointmentForDoctorAndInterval(Patient patient, Doctor doctor, DateTime startIntervalBound, DateTime endIntervalBound, DateTime deadline);
        public List<Appointment> FindFreeAppointments(Patient patient, DateTime deadline, DoctorSpecialization specialization, int freeAppointmentsCount);
        public List<Appointment> RecommendAppointments(Patient selectedPatient, Doctor selectedDoctor, DateTime startTime, DateTime endTime, DateTime deadline, string priority);
        public void MakeAppointment(Patient selectedPatient, Doctor selectedDoctor, DateTime startDateTime, DateTime endDateTime);
        public bool UpdateAppointment(Appointment appointment, Patient selectedPatient, Doctor selectedDoctor, DateTime startDateTime, DateTime endDateTime);
        public bool IsRoomAvailable(Room room, DateTime fromDate, DateTime toDate);
        public bool IsRoomAvailableForUpdate(Room room, DateTime fromDate, DateTime toDate, Appointment aptToUpdate);
        public bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate);
        public bool IsDoctorAvailableForUpdate(Doctor doctor, DateTime fromDate, DateTime toDate, Appointment aptToUpdate);
        public bool PatientHasAnAppointment(Guid patientId);
        public Room FindFreeRoom(RoomType roomType, DateTime start, DateTime end);
    }

}
