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

        public Appointment FindFirstFreeAppointmentForDoctorAndIntervalToDeadline(Patient patient, Doctor doctor, DateTime startTime, DateTime endTime, DateTime deadline) 
        {
            DateTime startDateTime = DateTime.Now.Date.AddSeconds(startTime.TimeOfDay.TotalSeconds);
            DateTime endDateTime = DateTime.Now.Date.AddSeconds(startTime.TimeOfDay.TotalSeconds).AddMinutes(15);
            DateTime upperBound = DateTime.Now.Date.AddSeconds(endTime.TimeOfDay.TotalSeconds);
            if (startDateTime < DateTime.Now) {
                startDateTime = startDateTime.AddDays(1);
                endDateTime = endDateTime.AddDays(1);
                upperBound = upperBound.AddDays(1);
            }
            if (startTime.TimeOfDay > endTime.TimeOfDay)
            {
                upperBound = upperBound.AddDays(1);
            }

            bool appointmentFound = false;
            Room emptyRoom = null;

            while (startDateTime.Date <= deadline.Date)
            {
                while (endDateTime <= upperBound)
                {
                    //doctor availabilty check
                    bool doctorAvailability = IsDoctorAvailable(doctor, startDateTime, endDateTime);
                    bool doctorRequestAvailability = _appointmentUpdateRequestService.IsDoctorAvailable(doctor, startDateTime, endDateTime);
                    if (doctorAvailability && doctorRequestAvailability)
                    {
                        //room availabilty check
                        emptyRoom = FindFreeRoom(RoomType.ExaminationRoom, startDateTime, endDateTime);
                        if (emptyRoom != null)
                        {
                            appointmentFound = true;
                            break;
                        }
                    }
                    startDateTime = startDateTime.AddMinutes(15);
                    endDateTime = endDateTime.AddMinutes(15);
                }
                if (appointmentFound && emptyRoom != null)
                {
                    break;
                }
                upperBound = upperBound.AddDays(1);
                startDateTime = startDateTime.AddDays(1).Date.AddSeconds(startTime.TimeOfDay.TotalSeconds);
                endDateTime = endDateTime.AddDays(1).Date.AddSeconds(startTime.TimeOfDay.TotalSeconds).AddMinutes(15);
            }

            if (appointmentFound && emptyRoom != null)
            {
                return new Appointment(doctor, patient, startDateTime, endDateTime, emptyRoom, null, false);
            }
            return null;
        }

        public Appointment FindFirstFreeAppointmentForDoctorToDeadline(Patient patient, Doctor doctor, DateTime deadline)
        {
            DateTime startDateTime = DateTime.Now.Date.AddMinutes(DateTime.Now.TimeOfDay.TotalMinutes).AddMinutes(60);
            startDateTime = startDateTime.AddSeconds(-startDateTime.Second);
            DateTime endDateTime = startDateTime.AddMinutes(15);

            bool appointmentFound = false;
            Room emptyRoom = null;

            while (startDateTime.Date <= deadline.Date)
            {
                //doctor availabilty check
                bool doctorAvailability = IsDoctorAvailable(doctor, startDateTime, endDateTime);
                bool doctorRequestAvailability = _appointmentUpdateRequestService.IsDoctorAvailable(doctor, startDateTime, endDateTime);
                if (doctorAvailability && doctorRequestAvailability)
                {
                    //room availabilty check
                    emptyRoom = FindFreeRoom(RoomType.ExaminationRoom, startDateTime, endDateTime);
                    if (emptyRoom != null)
                    {
                        appointmentFound = true;
                        break;
                    };
                }
                startDateTime = startDateTime.AddMinutes(15);
                endDateTime = endDateTime.AddMinutes(15);
            }
            if (appointmentFound && emptyRoom != null)
            {
                return new Appointment(doctor, patient, startDateTime, endDateTime, emptyRoom, null, false);
            }
            return null;
        }

        public Appointment FindFirstFreeAppointmentForIntervalToDeadline(Patient patient, DateTime startTime, DateTime endTime, DateTime deadline, DoctorSpecialization specialization) {
            DateTime startDateTime = DateTime.Now.Date.AddSeconds(startTime.TimeOfDay.TotalSeconds);
            DateTime endDateTime = DateTime.Now.Date.AddSeconds(startTime.TimeOfDay.TotalSeconds).AddMinutes(15);
            DateTime upperBound = DateTime.Now.Date.AddSeconds(endTime.TimeOfDay.TotalSeconds);
            if (startDateTime < DateTime.Now)
            {
                startDateTime = startDateTime.AddDays(1);
                endDateTime = endDateTime.AddDays(1);
                upperBound = upperBound.AddDays(1);
            }
            if (startTime.TimeOfDay > endTime.TimeOfDay)
            {
                upperBound = upperBound.AddDays(1);
            }

            bool appointmentFound = false;
            Room emptyRoom = null;
            Doctor freeDoctor = null;
            var doctors = _doctorService.ReadDoctorsWithSpecialization(specialization);


            while (startDateTime.Date <= deadline.Date)
            {
                while (endDateTime <= upperBound)
                {
                    foreach (var doctor in doctors)
                    {
                        //doctor availabilty check
                        bool doctorAvailability = IsDoctorAvailable(doctor, startDateTime, endDateTime);
                        bool doctorRequestAvailability = _appointmentUpdateRequestService.IsDoctorAvailable(doctor, startDateTime, endDateTime);
                        if (doctorAvailability && doctorRequestAvailability)
                        {
                            //room availabilty check
                            emptyRoom = FindFreeRoom(RoomType.ExaminationRoom, startDateTime, endDateTime);
                            if (emptyRoom != null)
                            {
                                freeDoctor = doctor;
                                appointmentFound = true;
                                break;
                            }
                        }
                    }
                    if (appointmentFound)
                    {
                        break;
                    }
                    startDateTime = startDateTime.AddMinutes(15);
                    endDateTime = endDateTime.AddMinutes(15);
                }
                if (appointmentFound)
                {
                    break;
                }
                upperBound = upperBound.AddDays(1);
                startDateTime = startDateTime.AddDays(1).Date.AddSeconds(startTime.TimeOfDay.TotalSeconds);
                endDateTime = endDateTime.AddDays(1).Date.AddSeconds(startTime.TimeOfDay.TotalSeconds).AddMinutes(15);
            }

            if (appointmentFound && emptyRoom != null)
            {
                return new Appointment(freeDoctor, patient, startDateTime, endDateTime, emptyRoom, null, false);
            }
            return null;
        }

        public List<Appointment> RecommendAppointments(Patient patient, Doctor doctor, DateTime startTime, DateTime endTime, DateTime deadline, string priority) 
        {
            try
            {
                Appointment apt = FindFirstFreeAppointmentForDoctorAndIntervalToDeadline(patient, doctor, startTime, endTime, deadline);
                if (apt != null)
                {
                    List<Appointment> tempList = new List<Appointment>();
                    tempList.Add(apt);
                    return tempList;
                }

                if (priority == "Doctor")
                {
                    apt = FindFirstFreeAppointmentForDoctorToDeadline(patient, doctor, deadline);
                    if (apt != null)
                    {
                        List<Appointment> tempList = new List<Appointment>();
                        tempList.Add(apt);
                        return tempList;
                    }
                    else
                    {
                        throw new RecommendationNotFoundException();
                    }
                }
                else if (priority == "Deadline")
                {
                    apt = FindFirstFreeAppointmentForIntervalToDeadline(patient,startTime, endTime, deadline, doctor.Specialization);
                    if (apt != null)
                    {
                        List<Appointment> tempList = new List<Appointment>();
                        tempList.Add(apt);
                        return tempList;
                    }
                    else 
                    {
                        throw new RecommendationNotFoundException();
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (RecommendationNotFoundException) 
            {
                return null;
            }
        }

        public void MakeAppointment(Patient selectedPatient, Doctor selectedDoctor, DateTime startDateTime, DateTime endDateTime)
        {

            //doctor availabilty check
            bool doctorAvailability = IsDoctorAvailable(selectedDoctor, startDateTime, endDateTime);
            bool doctorRequestAvailability = _appointmentUpdateRequestService.IsDoctorAvailable(selectedDoctor, startDateTime, endDateTime);
            if (!doctorAvailability || !doctorRequestAvailability)
            {
                throw new DoctorBusyException();
            }

            //room availabilty check
            Room emptyRoom = FindFreeRoom(RoomType.ExaminationRoom, startDateTime, endDateTime);
            if (emptyRoom == null)
            {
                throw new RoomBusyException();
            }

            Appointment app = new Appointment(selectedDoctor, selectedPatient, startDateTime, endDateTime, emptyRoom, null, false);
            Create(app);
        }

        public bool updateAppointment(Appointment selectedAppointment, Patient selectedPatient, Doctor selectedDoctor, DateTime startDateTime, DateTime endDateTime)
        {
            bool doctorAvailability = IsDoctorAvailableForUpdate(selectedDoctor, startDateTime, endDateTime, selectedAppointment);
            bool doctorRequestAvailability = _appointmentUpdateRequestService.IsDoctorAvailable(selectedDoctor, startDateTime, endDateTime);
            if (!doctorAvailability || !doctorRequestAvailability)
            {
                throw new DoctorBusyException();
            }

            //rooms availabilty check
            Room emptyRoom = FindFreeRoom(RoomType.ExaminationRoom, startDateTime, endDateTime);
            if (emptyRoom == null)
            {
                throw new RoomBusyException();
            }

            if (selectedAppointment.StartDate == startDateTime && selectedAppointment.EndDate == endDateTime
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
                    StartDate = startDateTime,
                    EndDate = endDateTime,
                    Doctor = selectedDoctor,
                    Room = emptyRoom
                };
                _appointmentUpdateRequestService.Create(appointmentRequest);
                return false;
            }
            else
            {
                selectedAppointment.StartDate = startDateTime;
                selectedAppointment.EndDate = endDateTime;
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

        public IEnumerable<Appointment> ReadFinishedAppointmentsForPatient(Patient pt)
        {
            return _entities.Where(ap => ap.IsDone == true)
                            .Where(apt => apt.Patient == pt);
        }

        public IEnumerable<Appointment> ReadPatientAppointments(Patient pt)
        {
            return _entities.Where(apt => apt.Patient == pt);
        }

        public IEnumerable<Appointment> ReadFuturePatientAppointments(Patient pt) 
        {
            return _entities.Where(apt => apt.Patient == pt && apt.StartDate > DateTime.Now);
        }
        public IEnumerable<Appointment> ReadPastPatientAppointments(Patient pt) 
        {
            return _entities.Where(apt => apt.Patient == pt && apt.StartDate < DateTime.Now);
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

        public Room FindFreeRoom(RoomType roomType, DateTime startDateTime, DateTime endDateTime) {
            var examinationRooms = _roomService.ReadRoomsWithType(roomType);
            Room emptyRoom = null;
            foreach (var room in examinationRooms)
            {
                bool roomAvailability = IsRoomAvailable(room, startDateTime, endDateTime);
                bool roomRequestAvailability = _appointmentUpdateRequestService.IsRoomAvailable(room, startDateTime, endDateTime);
                if (roomAvailability && roomRequestAvailability)
                {
                    emptyRoom = room;
                    break;
                }
            }
            return emptyRoom;
        }

        public IEnumerable<Appointment> FilterFinishedAppointmentsByAnamnesisSearchText(string text, Patient pt) { 
            return _entities.Where(apt => apt.Anamnesis.Contains(text) && apt.IsDone == true && apt.Patient == pt);
        }

        /// <summary>
        /// Check if patient is present in atleast one appointment
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public bool PatientHasAnAppointment(Guid patientId)
        {
            return (_entities.Where(apt => apt.Patient.Id == patientId).Count() != 0);
        }
    }
}
