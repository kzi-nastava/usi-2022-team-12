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
        public IEnumerable<Appointment> FilterFinishedAppointmentsByAnamnesisSearchText(string text, Patient pt);
        public IEnumerable<Appointment> GetAppointmentsForDateRangeAndDoctor(DateTime start, DateTime end, Doctor doctor);
        public Appointment FindFirstFreeAppointmentForDoctorToDeadline(Patient patient, Doctor doctor, DateTime deadline);
        public Appointment FindFirstFreeAppointmentForIntervalToDeadline(Patient patient, DateTime startTime, DateTime endTime, DateTime deadline, DoctorSpecialization specialization);
        public Appointment FindFirstFreeAppointmentForDoctorAndIntervalToDeadline(Patient patient, Doctor doctor, DateTime startTime, DateTime endTime, DateTime deadline);
        public List<Appointment> FindFreeAppointmentsToDeadline(Patient patient, DateTime deadline, DoctorSpecialization specialization, int numberOfAppointments);
        public List<Appointment> RecommendAppointments(Patient selectedPatient, Doctor selectedDoctor, DateTime startTime, DateTime endTime, DateTime deadline, string priority);
        public void MakeAppointment(Patient selectedPatient, Doctor selectedDoctor, DateTime startDateTime, DateTime endDateTime);
        public bool updateAppointment(Appointment appointment, Patient selectedPatient, Doctor selectedDoctor, DateTime startDateTime, DateTime endDateTime);
        public bool IsRoomAvailable(Room room, DateTime fromDate, DateTime toDate);
        public bool IsRoomAvailableForUpdate(Room room, DateTime fromDate, DateTime toDate, Appointment aptToUpdate);
        public Room FindFreeRoom(RoomType roomType, DateTime startDateTime, DateTime endDateTime);
        public bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate);
        public bool IsDoctorAvailableForUpdate(Doctor doctor, DateTime fromDate, DateTime toDate, Appointment aptToUpdate);
        public bool PatientHasAnAppointment(Guid patientId);
    }

}
