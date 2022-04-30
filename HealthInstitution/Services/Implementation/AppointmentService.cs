using HealthInstitution.Exceptions;
using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthInstitution.Services.Implementation
{
    public class AppointmentService : CrudService<Appointment>, IAppointmentService
    {
        private readonly IDoctorService _doctorService;
        private readonly IAppointmentUpdateRequestService _appointmentUpdateRequestService;
        private readonly IRoomService _roomService;
        public AppointmentService(DatabaseContext context, IDoctorService doctorService, IAppointmentUpdateRequestService appointmentUpdateRequestService, IRoomService roomService) : base(context)
        {
            _doctorService = doctorService;
            _appointmentUpdateRequestService = appointmentUpdateRequestService;
            _roomService = roomService;
        }

        public void MakeAppointment(Patient selectedPatient, Doctor selectedDoctor, DateTime startDate, DateTime endDate) {

            //doctor availabilty check
            bool doctorAvailability = IsDoctorAvailable(selectedDoctor, startDate, endDate);
            bool doctorRequestAvailability = _appointmentUpdateRequestService.IsDoctorAvailable(selectedDoctor, startDate, endDate);
            if (!doctorAvailability || !doctorRequestAvailability)
            {
                throw new DoctorBusyException();
            }

            //room availabilty check
            var examinationRooms = _roomService.ReadRoomsWithType(RoomType.ExaminationRoom);
            Room emptyRoom = null;
            foreach (var room in examinationRooms)
            {
                bool roomAvailability = IsRoomAvailable(room, startDate, endDate);
                bool roomRequestAvailability = _appointmentUpdateRequestService.IsRoomAvailable(room, startDate, endDate);
                if (roomAvailability && roomRequestAvailability)
                {
                    emptyRoom = room;
                    break;
                }
            }

            if (emptyRoom == null)
            {
                throw new RoomBusyException();
            }

            Appointment app = new Appointment(selectedDoctor, selectedPatient, startDate, endDate, emptyRoom, null, false);
            Create(app);
        }

        public bool updateAppointment(Appointment selectedAppointment, Patient selectedPatient, Doctor selectedDoctor, DateTime startDate, DateTime endDate) {
            bool doctorAvailability = IsDoctorAvailableForUpdate(selectedDoctor, startDate, endDate, selectedAppointment);
            bool doctorRequestAvailability = _appointmentUpdateRequestService.IsDoctorAvailable(selectedDoctor, startDate, endDate);
            if (!doctorAvailability || !doctorRequestAvailability)
            {
                throw new DoctorBusyException();
            }

            //rooms availabilty check
            var examinationRooms = _roomService.ReadRoomsWithType(RoomType.ExaminationRoom);
            Room emptyRoom = null;
            foreach (var room in examinationRooms)
            {
                bool roomAvailability = IsRoomAvailableForUpdate(room, startDate, endDate, selectedAppointment);
                bool roomRequestAvailability = _appointmentUpdateRequestService.IsRoomAvailable(room, startDate, endDate);
                if (roomAvailability && roomRequestAvailability)
                {
                    emptyRoom = room;
                    break;
                }
            }

            if (emptyRoom == null)
            {
                throw new RoomBusyException();
            }

            if (selectedAppointment.StartDate == startDate && selectedAppointment.EndDate == endDate
                && selectedAppointment.Doctor == selectedDoctor && selectedAppointment.Patient == selectedPatient)
            {
                throw new UpdateFailedException();
            }
                
            if (DateTime.Now.AddDays(2) > selectedAppointment.StartDate)
            {
                AppointmentUpdateRequest appointmentRequest = new AppointmentUpdateRequest
                {
                    Patient = selectedPatient,
                    Appointment = selectedAppointment,
                    ActivityType = ActivityType.Update,
                    Status = Status.Pending,
                    StartDate = startDate,
                    EndDate = endDate,
                    Doctor = selectedDoctor,
                    Room = emptyRoom
                };
                _appointmentUpdateRequestService.Create(appointmentRequest);
                return false;
            }
            else
            {
                selectedAppointment.StartDate = startDate;
                selectedAppointment.EndDate = endDate;
                selectedAppointment.Doctor = selectedDoctor;
                selectedAppointment.Room = emptyRoom;
                Update(selectedAppointment);
                return true;
            }
        }

        public IEnumerable<Appointment> GetAppointmentsForDateRangeAndDoctor(DateTime start, DateTime end, Doctor doctor)
        {
            return _entities.Where(e => e.Doctor == doctor && e.StartDate.Date >= start.Date && e.StartDate.Date <= end.Date);
        }

        public IEnumerable<Appointment> ReadPatientAppointments(Patient pt)
        {
            return _entities.Where(apt => apt.Patient == pt).ToList();
        }

        public IEnumerable<Appointment> ReadRoomAppointments(Room r)
        {
            return _entities.Where(apt => apt.Room == r).ToList();
        }
        public bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate)
        {
            return (_entities.Where(apt => apt.Doctor == doctor && apt.StartDate < toDate && fromDate < apt.EndDate).Count() == 0);
        }
        public bool IsDoctorAvailableForUpdate(Doctor doctor, DateTime fromDate, DateTime toDate, Appointment aptToUpdate)
        {
            return ((_entities.Where(apt => apt != aptToUpdate && apt.Doctor == doctor && apt.StartDate < toDate && fromDate < apt.EndDate)).Count() == 0);
        }

        public bool IsRoomAvailable(Room room, DateTime fromDate, DateTime toDate)
        {
            return ((_entities.Where(apt => apt.Room == room && apt.StartDate < toDate && fromDate < apt.EndDate)).Count() == 0);
        }
        public bool IsRoomAvailableForUpdate(Room room, DateTime fromDate, DateTime toDate, Appointment aptToUpdate)
        {
            return ((_entities.Where(apt => apt.Room == room && apt != aptToUpdate && apt.StartDate < toDate && fromDate < apt.EndDate)).Count() == 0);
        }
    }
}
