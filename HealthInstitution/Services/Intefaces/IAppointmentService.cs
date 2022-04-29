using HealthInstitution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services.Intefaces
{
    public interface IAppointmentService : ICrudService<Appointment> {
        IEnumerable<Appointment> ReadPatientAppointments(Patient pt);
        void makeAppointment(Patient selectedPatient, Doctor selectedDoctor, DateTime startDate, DateTime endDate);
        bool updateAppointment(Appointment appointment, Patient selectedPatient, Doctor selectedDoctor, DateTime startDate, DateTime endDate);
        bool IsRoomAvailable(Room room, DateTime fromDate, DateTime toDate);
        bool IsRoomAvailableForUpdate(Room room, DateTime fromDate, DateTime toDate, Appointment aptToUpdate);
        bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate);
        bool IsDoctorAvailableForUpdate(Doctor doctor, DateTime fromDate, DateTime toDate, Appointment aptToUpdate);
    }

}
