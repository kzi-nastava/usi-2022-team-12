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
        public IEnumerable<Appointment> FindFinishedAppointmentsWithAnamnesis(Patient patient, string searchText);
        public IEnumerable<Appointment> GetAppointmentsForDateRangeAndDoctor(DateTime start, DateTime end, Doctor doctor);
        public Appointment FindFreeAppointmentForDoctorInTimeSpan(Patient patient, List<Doctor> doctors, DateTime intervalStart, DateTime intervalEnd);
        public Appointment FindFreeAppointmentForDoctorsInTimeInterval(Patient patient, List<Doctor> doctors, TimeOnly intervalStart, TimeOnly intervalEnd, DateOnly deadline);
        public Appointment FindFreeAppointmentInTimeInterval(Patient patient, DoctorSpecialization specialization, TimeOnly intervalStart, TimeOnly intervalEnd, DateOnly deadline);
        public Appointment FindFreeAppointmentForDoctors(Patient patient, List<Doctor> doctors, DateOnly deadline);
        public List<Appointment> FindFreeAppointmentsForDoctorSpecialization(Patient patient, DoctorSpecialization specialization, DateOnly deadline, int expectedNumber);
        public List<Appointment> RecommendAppointments(Patient patient, Doctor doctor, TimeOnly startTime, TimeOnly endTime, DateOnly deadline, string priority);
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
