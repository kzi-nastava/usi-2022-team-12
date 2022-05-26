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
        private readonly IRoomRenovationService _roomRenovationService;
        public AppointmentService(DatabaseContext context, IDoctorService doctorService, IAppointmentUpdateRequestService appointmentUpdateRequestService, IRoomService roomService, IRoomRenovationService roomRenovationService) : base(context)
        {
            _doctorService = doctorService;
            _appointmentUpdateRequestService = appointmentUpdateRequestService;
            _roomService = roomService;
            _roomRenovationService = roomRenovationService;
        }

        #region To be changed

        // FUNCTIONS WAITING FURTHER ANALYSIS
        public IList<Appointment> FindPossibleAppointmentsForDoctorSpecialization(Patient currentPatient, DateTime deadline, DoctorSpecialization specialization, int expectedNumber)
        {
            var specializedDoctors = _doctorService.FindDoctorsWithSpecialization(specialization);
            IList<Appointment> possibleAppointments = new List<Appointment>();

            for (DateTime potentialTime = DateTime.Now.AddMinutes(15); potentialTime <= deadline; potentialTime = potentialTime.AddMinutes(1))
            {
                DateTime startTime = potentialTime;
                DateTime endTime = potentialTime.AddMinutes(15);

                foreach (var doctor in specializedDoctors)
                {
                    if (!IsDoctorAvailable(doctor, startTime, endTime) ||
                        !_appointmentUpdateRequestService.IsDoctorAvailable(doctor, startTime, endTime))
                        continue;

                    Room freeRoom = FindFreeRoom(RoomType.ExaminationRoom, startTime, endTime);
                    if (freeRoom == null)
                        continue;

                    possibleAppointments.Add(new Appointment
                    {
                        Doctor = doctor,
                        Patient = currentPatient,
                        StartDate = startTime,
                        EndDate = endTime,
                        Room = freeRoom,
                        AppointmentType = AppointmentType.Regular,
                        IsDone = false,
                        Anamnesis = null
                    });

                    if (possibleAppointments.Count > expectedNumber)
                        return possibleAppointments;
                }
            }

            return possibleAppointments;
        }

        public Appointment FindAppointmentForDoctorUntilDeadline(Patient currentPatient, Doctor currentDoctor, DateTime deadline)
        {
            return FindAppointmentForDoctorInTimeSpan(currentPatient, currentDoctor, DateTime.Now.AddMinutes(15), deadline);
        }

        public Appointment FindAppointmentForDoctorInTimeInterval(Patient currentPatient, Doctor currentDoctor, DateTime intervalStart, DateTime intervalEnd, DateTime deadline)
        {
            DateTime today = DateTime.Now;
            DateTime startDate = intervalStart.Hour < today.Hour ? today.Date : today.Date.AddDays(1);

            for (DateTime currentDate = startDate; currentDate <= deadline; currentDate = currentDate.AddDays(1))
            {
                DateTime intervalDateStart = currentDate.AddHours(intervalStart.Hour)
                                                        .AddMinutes(intervalStart.Minute);

                DateTime intervalDateEnd = currentDate.AddHours(intervalEnd.Hour)
                                                      .AddMinutes(intervalEnd.Minute);

                Appointment foundAppointment = FindAppointmentForDoctorInTimeSpan(currentPatient, currentDoctor, intervalDateStart, intervalDateEnd);

                if (foundAppointment != null)
                    return foundAppointment;
            }

            return null;
        }

        public Appointment FindAppointmentForDoctorInTimeSpan(Patient currentPatient, Doctor currentDoctor, DateTime intervalStart, DateTime intervalEnd)
        {
            for (DateTime potentialTime = intervalStart; potentialTime <= intervalEnd; potentialTime = potentialTime.AddMinutes(1))
            {
                DateTime startTime = potentialTime;
                DateTime endTime = potentialTime.AddMinutes(15);

                if (!IsDoctorAvailable(currentDoctor, startTime, endTime) ||
                    !_appointmentUpdateRequestService.IsDoctorAvailable(currentDoctor, startTime, endTime))
                    continue;

                Room freeRoom = FindFreeRoom(RoomType.ExaminationRoom, startTime, endTime);
                if (freeRoom == null)
                    continue;

                return new Appointment
                {
                    Doctor = currentDoctor,
                    Patient = currentPatient,
                    StartDate = startTime,
                    EndDate = endTime,
                    Room = freeRoom,
                    AppointmentType = AppointmentType.Regular,
                    IsDone = false,
                    Anamnesis = null
                };
            }

            return null;
        }

        #endregion

        public Appointment FindFirstFreeAppointmentForDoctorAndInterval(Patient patient, Doctor doctor, DateTime startIntervalBound, DateTime endIntervalBound, DateTime deadline)
        {
            DateTime start = DateTime.Now.Date.AddMinutes(startIntervalBound.TimeOfDay.TotalMinutes);
            DateTime end = DateTime.Now.Date.AddMinutes(startIntervalBound.TimeOfDay.TotalMinutes).AddMinutes(15);
            DateTime upperIntervalBound = DateTime.Now.Date.AddMinutes(endIntervalBound.TimeOfDay.TotalMinutes);

            if (start < DateTime.Now)
            {
                start = start.AddDays(1);
                end = end.AddDays(1);
                upperIntervalBound = upperIntervalBound.AddDays(1);
            }
            if (startIntervalBound.TimeOfDay > endIntervalBound.TimeOfDay)
            {
                upperIntervalBound = upperIntervalBound.AddDays(1);
            }

            while (end.Date <= deadline.Date)
            {
                while (end <= upperIntervalBound)
                {
                    //doctor availabilty check
                    if (IsDoctorAvailable(doctor, start, end) && _appointmentUpdateRequestService.IsDoctorAvailable(doctor, start, end))
                    {
                        //room availabilty check
                        Room emptyRoom = FindFreeRoom(RoomType.ExaminationRoom, start, end);
                        if (emptyRoom != null)
                        {
                            return new Appointment(doctor, patient, start, end, emptyRoom, null, false);
                        }
                    }
                    start = start.AddMinutes(1);
                    end = end.AddMinutes(1);
                }
                upperIntervalBound = upperIntervalBound.AddDays(1);
                start = start.AddDays(1).Date.AddMinutes(startIntervalBound.TimeOfDay.TotalMinutes);
                end = end.AddDays(1).Date.AddMinutes(startIntervalBound.TimeOfDay.TotalMinutes).AddMinutes(15);
            }
            return null;
        }

        public Appointment FindFirstFreeAppointmentForDoctor(Patient patient, Doctor doctor, DateTime deadline)
        {
            DateTime CurrentDateTime = DateTime.Now;
            DateTime start = CurrentDateTime.Date.AddHours(CurrentDateTime.Hour).AddMinutes(CurrentDateTime.Minute + 60);
            DateTime end = start.AddMinutes(15);

            while (end.Date <= deadline.Date)
            {
                //doctor availabilty check
                if (IsDoctorAvailable(doctor, start, end) && _appointmentUpdateRequestService.IsDoctorAvailable(doctor, start, end))
                {
                    //room availabilty check
                    Room emptyRoom = FindFreeRoom(RoomType.ExaminationRoom, start, end);
                    if (emptyRoom != null)
                    {
                        return new Appointment(doctor, patient, start, end, emptyRoom, null, false);
                    }
                }
                start = start.AddMinutes(1);
                end = end.AddMinutes(1);
            }

            return null;
        }

        public Appointment FindFirstFreeAppointmentForInterval(Patient patient, DoctorSpecialization specialization, DateTime startIntervalBound, DateTime endIntervalBound, DateTime deadline)
        {
            DateTime start = DateTime.Now.Date.AddMinutes(startIntervalBound.TimeOfDay.TotalMinutes);
            DateTime end = DateTime.Now.Date.AddMinutes(startIntervalBound.TimeOfDay.TotalMinutes).AddMinutes(15);
            DateTime upperIntervalBound = DateTime.Now.Date.AddMinutes(endIntervalBound.TimeOfDay.TotalMinutes);
            if (start < DateTime.Now)
            {
                start = start.AddDays(1);
                end = end.AddDays(1);
                upperIntervalBound = upperIntervalBound.AddDays(1);
            }
            if (startIntervalBound.TimeOfDay.TotalMinutes > endIntervalBound.TimeOfDay.TotalMinutes)
            {
                upperIntervalBound = upperIntervalBound.AddDays(1);
            }

            var doctors = _doctorService.FindDoctorsWithSpecialization(specialization);
            while (end.Date <= deadline.Date)
            {
                while (end <= upperIntervalBound)
                {
                    foreach (var doctor in doctors)
                    {
                        //doctor availabilty check
                        if (IsDoctorAvailable(doctor, start, end) && _appointmentUpdateRequestService.IsDoctorAvailable(doctor, start, end))
                        {
                            //room availabilty check
                            Room emptyRoom = FindFreeRoom(RoomType.ExaminationRoom, start, end);
                            if (emptyRoom != null)
                            {
                                return new Appointment(doctor, patient, start, end, emptyRoom, null, false);
                            }
                        }
                    }
                    start = start.AddMinutes(1);
                    end = end.AddMinutes(1);
                }
                upperIntervalBound = upperIntervalBound.AddDays(1);
                start = start.AddDays(1).Date.AddMinutes(startIntervalBound.TimeOfDay.TotalMinutes);
                end = end.AddDays(1).Date.AddMinutes(startIntervalBound.TimeOfDay.TotalMinutes).AddMinutes(15);
            }
            return null;
        }

        public List<Appointment> FindFreeAppointments(Patient patient, DateTime deadline, DoctorSpecialization specialization, int freeAppointmentsCount)
        {
            DateTime CurrentDateTime = DateTime.Now;
            DateTime start = CurrentDateTime.Date.AddHours(CurrentDateTime.Hour).AddMinutes(CurrentDateTime.Minute + 60);
            DateTime end = start.AddMinutes(15);

            var doctors = _doctorService.FindDoctorsWithSpecialization(specialization);
            List<Appointment> freeAppointments = new List<Appointment>();

            while (end.Date <= deadline.Date)
            {
                foreach (var doctor in doctors)
                {
                    //doctor availabilty check
                    if (IsDoctorAvailable(doctor, start, end) && _appointmentUpdateRequestService.IsDoctorAvailable(doctor, start, end))
                    {
                        //room availabilty check
                        Room emptyRoom = FindFreeRoom(RoomType.ExaminationRoom, start, end);
                        if (emptyRoom != null)
                        {
                            freeAppointments.Add(new Appointment(doctor, patient, start, end, emptyRoom, null, false));
                            start = start.AddMinutes(15);
                            end = end.AddMinutes(15);
                        }
                    }

                    if (freeAppointments.Count >= freeAppointmentsCount)
                    {
                        return freeAppointments;
                    }
                }
                start = start.AddMinutes(1);
                end = end.AddMinutes(1);
            }
            return freeAppointments;
        }

        public List<Appointment> RecommendAppointments(Patient patient, Doctor doctor, DateTime startTime, DateTime endTime, DateTime deadline, string priority)
        {
            try
            {
                Appointment suggestedAppointment = FindFirstFreeAppointmentForDoctorAndInterval(patient, doctor, startTime, endTime, deadline);
                if (suggestedAppointment != null)
                {
                    List<Appointment> tempList = new List<Appointment>();
                    tempList.Add(suggestedAppointment);
                    return tempList;
                }

                if (priority == "Doctor")
                {
                    suggestedAppointment = FindFirstFreeAppointmentForDoctor(patient, doctor, deadline);
                    if (suggestedAppointment != null)
                    {
                        List<Appointment> tempList = new List<Appointment>();
                        tempList.Add(suggestedAppointment);
                        return tempList;
                    }
                }
                else if (priority == "TimeInterval")
                {
                    suggestedAppointment = FindFirstFreeAppointmentForInterval(patient, doctor.Specialization, startTime, endTime, deadline);
                    if (suggestedAppointment != null)
                    {
                        List<Appointment> tempList = new List<Appointment>();
                        tempList.Add(suggestedAppointment);
                        return tempList;
                    }
                }
                throw new RecommendationNotFoundException();
            }
            catch (RecommendationNotFoundException)
            {
                List<Appointment> suggestedAppointment = FindFreeAppointments(patient, deadline, doctor.Specialization, 3);
                if (suggestedAppointment.Count != 0)
                {
                    return suggestedAppointment;
                }
                return null;
            }
        }

        public void MakeAppointment(Patient selectedPatient, Doctor selectedDoctor, DateTime startDateTime, DateTime endDateTime)
        {
            //doctor availabilty check
            if (!IsDoctorAvailable(selectedDoctor, startDateTime, endDateTime) ||
                !_appointmentUpdateRequestService.IsDoctorAvailable(selectedDoctor, startDateTime, endDateTime))
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

        public bool UpdateAppointment(Appointment selectedAppointment, Patient selectedPatient, Doctor selectedDoctor, DateTime startDateTime, DateTime endDateTime)
        {
            if (!IsDoctorAvailableForUpdate(selectedDoctor, startDateTime, endDateTime, selectedAppointment) ||
                !_appointmentUpdateRequestService.IsDoctorAvailable(selectedDoctor, startDateTime, endDateTime))
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

            selectedAppointment.StartDate = startDateTime;
            selectedAppointment.EndDate = endDateTime;
            selectedAppointment.Doctor = selectedDoctor;
            selectedAppointment.Room = emptyRoom;
            Update(selectedAppointment);

            return true;
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
            return _entities.Where(apt => apt.Patient == pt && apt.StartDate > DateTime.Now && apt.IsDone == false);
        }

        public IEnumerable<Appointment> ReadRoomAppointments(Room r)
        {
            return _entities.Where(apt => apt.Room == r).ToList();
        }

        public bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate)
        {
            return _entities.Where(apt => apt.Doctor == doctor && apt.StartDate < toDate && fromDate < apt.EndDate).Count() == 0;
        }

        public bool IsDoctorAvailableForUpdate(Doctor doctor, DateTime fromDate, DateTime toDate, Appointment aptToUpdate)
        {
            return _entities.Where(apt => apt != aptToUpdate && apt.Doctor == doctor && apt.StartDate < toDate && fromDate < apt.EndDate).Count() == 0;
        }

        public bool IsRoomAvailable(Room room, DateTime fromDate, DateTime toDate)
        {
            return _entities.Where(apt => apt.Room == room && apt.StartDate < toDate && fromDate < apt.EndDate).Count() == 0;
        }

        public bool IsRoomAvailableForUpdate(Room room, DateTime fromDate, DateTime toDate, Appointment aptToUpdate)
        {
            return _entities.Where(apt => apt.Room == room && apt != aptToUpdate && apt.StartDate < toDate && fromDate < apt.EndDate).Count() == 0;
        }

        public Room FindFreeRoom(RoomType roomType, DateTime start, DateTime end)
        {
            var examinationRooms = _roomService.ReadRoomsWithType(roomType);
            foreach (var room in examinationRooms)
            {
                if (IsRoomAvailable(room, start, end) && _appointmentUpdateRequestService.IsRoomAvailable(room, start, end)
                    && _roomRenovationService.IsRoomNotRenovating(room, start, end))
                {
                    return room;
                }
            }
            return null;
        }

        public IEnumerable<Appointment> FindFinishedAppointmentsWithAnamnesis(Patient patient, string searchText) {
            searchText = searchText.ToLower();
            return _entities.Where(apt => apt.Anamnesis.ToLower().Contains(searchText) && apt.IsDone == true && apt.Patient == patient);
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
