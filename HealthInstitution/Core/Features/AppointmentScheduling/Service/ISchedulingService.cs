using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Service
{
    public interface ISchedulingService
    {
        IEnumerable<Appointment> FindFinishedAppointmentsWithAnamnesis(Patient patient, string searchText);
        IEnumerable<Appointment> GetAppointmentsForDateRangeAndDoctor(DateTime start, DateTime end, Doctor doctor);
        void MakeAppointment(Patient selectedPatient, Doctor selectedDoctor, DateTime startDateTime, DateTime endDateTime, AppointmentType appointmentType);
        bool PatientHasAnAppointment(Guid patientId);
        IEnumerable<Appointment> ReadFinishedAppointmentsForPatient(Patient pt);
        IEnumerable<Appointment> ReadFuturePatientAppointments(Patient pt);
        IEnumerable<Appointment> ReadPatientAppointments(Patient pt);
        IEnumerable<Appointment> ReadRoomAppointments(Room r);
        List<Appointment> RecommendAppointments(Patient patient, Doctor doctor, TimeOnly startTime, TimeOnly endTime, DateOnly deadline, string priority);
        bool UpdateAppointment(Appointment selectedAppointment, Patient selectedPatient, Doctor selectedDoctor, DateTime startDateTime, DateTime endDateTime);
    }
}