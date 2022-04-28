using HealthInstitution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services.Intefaces
{
    public interface IAppointmentService : ICrudService<Appointment> {
        IEnumerable<Appointment> ReadDoctorAppointemnts(Doctor doc, DateTime fromDate, DateTime toDate);
        IEnumerable<Appointment> ReadDoctorAppointemntsWithoutChosen(Doctor doc, DateTime fromDate, DateTime toDate, Appointment chosenAppointment);
        IEnumerable<Appointment> ReadAppointemntsInInterval(DateTime fromDate, DateTime toDate);
        IEnumerable<Appointment> ReadAppointemntsInIntervalWithoutChosen(DateTime fromDate, DateTime toDate, Appointment chosenAppointment);
        IEnumerable<Appointment> ReadPatientAppointments(Patient pt);
        IEnumerable<Appointment> ReadRoomAppointments(Room r);
    }

}
