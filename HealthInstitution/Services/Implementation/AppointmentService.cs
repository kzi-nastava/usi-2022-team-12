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

        //#region backup methods
        //public Appointment FindFirstFreeAppointmentForDoctorAndInterval(Patient patient, Doctor doctor, TimeOnly startIntervalBound, TimeOnly endIntervalBound, DateOnly deadline)
        //{
        //    DateTime start = DateTime.Now.Date.AddTicks(startIntervalBound.Ticks);
        //    DateTime end = DateTime.Now.Date.AddTicks(startIntervalBound.Ticks).AddMinutes(15);
        //    DateTime upperIntervalBound = DateTime.Now.Date.AddTicks(endIntervalBound.Ticks);

        //    if (start < DateTime.Now)
        //    {
        //        start = start.AddDays(1);
        //        end = end.AddDays(1);
        //        upperIntervalBound = upperIntervalBound.AddDays(1);
        //    }
        //    if (startIntervalBound > endIntervalBound)
        //    {
        //        upperIntervalBound = upperIntervalBound.AddDays(1);
        //    }

        //    while (DateOnly.FromDateTime(end) <= deadline)
        //    {
        //        while (end <= upperIntervalBound)
        //        {
        //            //doctor availabilty check
        //            if (IsDoctorAvailable(doctor, start, end) && _appointmentUpdateRequestService.IsDoctorAvailable(doctor, start, end))
        //            {
        //                //room availabilty check
        //                Room emptyRoom = FindFreeRoom(RoomType.ExaminationRoom, start, end);
        //                if (emptyRoom != null)
        //                {
        //                    return new Appointment(doctor, patient, start, end, emptyRoom, AppointmentType.Regular, null, false);
        //                }
        //            }
        //            start = start.AddMinutes(1);
        //            end = end.AddMinutes(1);
        //        }
        //        upperIntervalBound = upperIntervalBound.AddDays(1);
        //        start = start.AddDays(1).Date.AddTicks(startIntervalBound.Ticks);
        //        end = end.AddDays(1).Date.AddTicks(startIntervalBound.Ticks).AddMinutes(15);
        //    }
        //    return null;
        //}

        //public Appointment FindFirstFreeAppointmentForDoctor(Patient patient, Doctor doctor, DateOnly deadline)
        //{
        //    DateTime CurrentDateTime = DateTime.Now;
        //    DateTime start = CurrentDateTime.Date.AddHours(CurrentDateTime.Hour).AddMinutes(CurrentDateTime.Minute + 60);
        //    DateTime end = start.AddMinutes(15);

        //    while (DateOnly.FromDateTime(end) <= deadline)
        //    {
        //        //doctor availabilty check
        //        if (IsDoctorAvailable(doctor, start, end) && _appointmentUpdateRequestService.IsDoctorAvailable(doctor, start, end))
        //        {
        //            //room availabilty check
        //            Room emptyRoom = FindFreeRoom(RoomType.ExaminationRoom, start, end);
        //            if (emptyRoom != null)
        //            {
        //                return new Appointment(doctor, patient, start, end, emptyRoom, AppointmentType.Regular, null, false);
        //            }
        //        }
        //        start = start.AddMinutes(1);
        //        end = end.AddMinutes(1);
        //    }

        //    return null;
        //}

        //public Appointment FindFirstFreeAppointmentForInterval(Patient patient, DoctorSpecialization specialization, TimeOnly startIntervalBound, TimeOnly endIntervalBound, DateOnly deadline)
        //{
        //    DateTime start = DateTime.Now.Date.AddTicks(startIntervalBound.Ticks);
        //    DateTime end = DateTime.Now.Date.AddTicks(startIntervalBound.Ticks).AddMinutes(15);
        //    DateTime upperIntervalBound = DateTime.Now.Date.AddTicks(endIntervalBound.Ticks);
        //    if (start < DateTime.Now)
        //    {
        //        start = start.AddDays(1);
        //        end = end.AddDays(1);
        //        upperIntervalBound = upperIntervalBound.AddDays(1);
        //    }
        //    if (startIntervalBound > endIntervalBound)
        //    {
        //        upperIntervalBound = upperIntervalBound.AddDays(1);
        //    }

        //    var doctors = _doctorService.FindDoctorsWithSpecialization(specialization);
        //    while (DateOnly.FromDateTime(end) <= deadline)
        //    {
        //        while (end <= upperIntervalBound)
        //        {
        //            foreach (var doctor in doctors)
        //            {
        //                //doctor availabilty check
        //                if (IsDoctorAvailable(doctor, start, end) && _appointmentUpdateRequestService.IsDoctorAvailable(doctor, start, end))
        //                {
        //                    //room availabilty check
        //                    Room emptyRoom = FindFreeRoom(RoomType.ExaminationRoom, start, end);
        //                    if (emptyRoom != null)
        //                    {
        //                        return new Appointment(doctor, patient, start, end, emptyRoom, AppointmentType.Regular, null, false);
        //                    }
        //                }
        //            }
        //            start = start.AddMinutes(1);
        //            end = end.AddMinutes(1);
        //        }
        //        upperIntervalBound = upperIntervalBound.AddDays(1);
        //        start = start.AddDays(1).Date.AddTicks(startIntervalBound.Ticks);
        //        end = end.AddDays(1).Date.AddTicks(startIntervalBound.Ticks).AddMinutes(15);
        //    }
        //    return null;
        //}

        //public List<Appointment> FindFreeAppointments(Patient patient, DateOnly deadline, DoctorSpecialization specialization, int freeAppointmentsCount)
        //{
        //    DateTime CurrentDateTime = DateTime.Now;
        //    DateTime start = CurrentDateTime.Date.AddHours(CurrentDateTime.Hour).AddMinutes(CurrentDateTime.Minute + 60);
        //    DateTime end = start.AddMinutes(15);

        //    var doctors = _doctorService.FindDoctorsWithSpecialization(specialization);
        //    List<Appointment> freeAppointments = new List<Appointment>();

        //    while (DateOnly.FromDateTime(end) <= deadline)
        //    {
        //        foreach (var doctor in doctors)
        //        {
        //            //doctor availabilty check
        //            if (IsDoctorAvailable(doctor, start, end) && _appointmentUpdateRequestService.IsDoctorAvailable(doctor, start, end))
        //            {
        //                //room availabilty check
        //                Room emptyRoom = FindFreeRoom(RoomType.ExaminationRoom, start, end);
        //                if (emptyRoom != null)
        //                {
        //                    freeAppointments.Add(new Appointment(doctor, patient, start, end, emptyRoom, AppointmentType.Regular, null, false));
        //                    start = start.AddMinutes(15);
        //                    end = end.AddMinutes(15);
        //                }
        //            }

        //            if (freeAppointments.Count >= freeAppointmentsCount)
        //            {
        //                return freeAppointments;
        //            }
        //        }
        //        start = start.AddMinutes(1);
        //        end = end.AddMinutes(1);
        //    }
        //    return freeAppointments;
        //}
        //#endregion

        public Appointment FindFreeAppointmentForDoctorInTimeSpan(Patient patient, List<Doctor> doctors, DateTime intervalStart, DateTime intervalEnd)
        {
            for (DateTime potentialStart = intervalStart; potentialStart.AddMinutes(15) <= intervalEnd; potentialStart = potentialStart.AddMinutes(1))
            {
                DateTime appointmentStart = potentialStart;
                DateTime appointmentEnd = potentialStart.AddMinutes(15);

                foreach (var doctor in doctors)
                {
                    if (!IsDoctorAvailable(doctor, appointmentStart, appointmentEnd) ||
                        !_appointmentUpdateRequestService.IsDoctorAvailable(doctor, appointmentStart, appointmentEnd))
                        continue;

                    Room freeRoom = FindFreeRoom(RoomType.ExaminationRoom, appointmentStart, appointmentEnd);
                    if (freeRoom == null)
                        continue;

                    return new Appointment
                    {
                        Doctor = doctor,
                        Patient = patient,
                        StartDate = appointmentStart,
                        EndDate = appointmentEnd,
                        Room = freeRoom,
                        AppointmentType = AppointmentType.Regular,
                        Anamnesis = "",
                        IsDone = false,
                        IsRated = false,
                        PrescribedMedicines = new List<PrescribedMedicine>()
                    };
                }
            }
            return null;
        }

        public Appointment FindFreeAppointmentForDoctorsInTimeInterval(Patient patient, List<Doctor> doctors, TimeOnly intervalStart, TimeOnly intervalEnd, DateOnly deadline)
        {
            DateTime today = DateTime.Now;
            DateTime searchStartDate = today.Date.AddTicks(intervalStart.Ticks);

            if (searchStartDate < today) {
                searchStartDate = searchStartDate.AddDays(1);
            }

            for (DateTime potentialDate = searchStartDate; DateOnly.FromDateTime(potentialDate) <= deadline; potentialDate = potentialDate.AddDays(1))
            {
                DateTime dailyIntervalStart = potentialDate.Date.AddTicks(intervalStart.Ticks);
                DateTime dailyIntervalEnd = potentialDate.Date.AddTicks(intervalEnd.Ticks);

                if (intervalStart > intervalEnd) {
                    dailyIntervalEnd = dailyIntervalEnd.AddDays(1);
                }

                Appointment appointment = FindFreeAppointmentForDoctorInTimeSpan(patient, doctors, dailyIntervalStart, dailyIntervalEnd);

                if (appointment != null)
                    return appointment;
            }
            return null;
        }

        public Appointment FindFreeAppointmentInTimeInterval(Patient patient, DoctorSpecialization specialization, TimeOnly intervalStart, TimeOnly intervalEnd, DateOnly deadline)
        {
            List<Doctor> specializedDoctors = _doctorService.FindDoctorsWithSpecialization(specialization).ToList();
            return FindFreeAppointmentForDoctorsInTimeInterval(patient, specializedDoctors, intervalStart, intervalEnd, deadline);
        }

        public Appointment FindFreeAppointmentForDoctors(Patient patient, List<Doctor> doctors, DateOnly deadline)
        {
            DateTime today = DateTime.Now;
            return FindFreeAppointmentForDoctorInTimeSpan(patient, doctors, today.Date.AddHours(today.Hour).AddMinutes(today.Minute + 60), deadline.ToDateTime(TimeOnly.Parse("12:00 PM")));
        }

        public List<Appointment> FindFreeAppointmentsForDoctorSpecialization(Patient patient, DoctorSpecialization specialization, DateOnly deadline, int expectedNumber)
        {
            var specializedDoctors = _doctorService.FindDoctorsWithSpecialization(specialization);
            List<Appointment> freeAppointments = new List<Appointment>();
            DateTime today = DateTime.Now;

            for (DateTime potentialTime = today.Date.AddHours(today.Hour).AddMinutes(today.Minute + 60); DateOnly.FromDateTime(potentialTime.AddMinutes(15)) <= deadline; potentialTime = potentialTime.AddMinutes(1))
            {
                DateTime appointmentStart = potentialTime;
                DateTime appointmentEnd = potentialTime.AddMinutes(15);

                foreach (var doctor in specializedDoctors)
                {
                    if (!IsDoctorAvailable(doctor, appointmentStart, appointmentEnd) ||
                        !_appointmentUpdateRequestService.IsDoctorAvailable(doctor, appointmentStart, appointmentEnd))
                        continue;

                    Room freeRoom = FindFreeRoom(RoomType.ExaminationRoom, appointmentStart, appointmentEnd);
                    if (freeRoom == null)
                        continue;

                    freeAppointments.Add(new Appointment
                    {
                        Doctor = doctor,
                        Patient = patient,
                        StartDate = appointmentStart,
                        EndDate = appointmentEnd,
                        Room = freeRoom,
                        AppointmentType = AppointmentType.Regular,
                        Anamnesis = "",
                        IsDone = false,
                        IsRated = false,
                        PrescribedMedicines = new List<PrescribedMedicine>()
                    });
                    potentialTime = potentialTime.AddMinutes(15);

                    if (freeAppointments.Count >= expectedNumber)
                        return freeAppointments;
                }
            }
            return null;
        }

        public List<Appointment> RecommendAppointments(Patient patient, Doctor doctor, TimeOnly startTime, TimeOnly endTime, DateOnly deadline, string priority)
        {
            try
            {
                Appointment suggestedAppointment = FindFreeAppointmentForDoctorsInTimeInterval(patient, new List<Doctor> { doctor }, startTime, endTime, deadline);
                if (suggestedAppointment == null)
                {
                    if (priority == "Doctor")
                    {
                        suggestedAppointment = FindFreeAppointmentForDoctors(patient, new List<Doctor> { doctor }, deadline);
                    }
                    else if (priority == "TimeInterval")
                    {
                        suggestedAppointment = FindFreeAppointmentInTimeInterval(patient, doctor.Specialization, startTime, endTime, deadline);
                    }
                }

                if (suggestedAppointment != null)
                {
                    List<Appointment> tempList = new List<Appointment>();
                    tempList.Add(suggestedAppointment);
                    return tempList;
                }
                throw new RecommendationNotFoundException();
            }
            catch (RecommendationNotFoundException)
            {
                List<Appointment> suggestedAppointment = FindFreeAppointmentsForDoctorSpecialization(patient, doctor.Specialization, deadline, 3);
                if (suggestedAppointment != null)
                {
                    return suggestedAppointment;
                }
                return null;
            }
        }

        public void MakeAppointment(Patient selectedPatient, Doctor selectedDoctor, DateTime startDateTime, DateTime endDateTime, AppointmentType appointmentType)
        {
            //doctor availabilty check
            if (!IsDoctorAvailable(selectedDoctor, startDateTime, endDateTime) ||
                !_appointmentUpdateRequestService.IsDoctorAvailable(selectedDoctor, startDateTime, endDateTime))
            {
                throw new DoctorBusyException();
            }

            //room availabilty check
            RoomType roomType = appointmentType == AppointmentType.Regular ? RoomType.ExaminationRoom : RoomType.OperationRoom;
            Room emptyRoom = FindFreeRoom(roomType, startDateTime, endDateTime);
            if (emptyRoom == null)
            {
                throw new RoomBusyException();
            }

            Appointment app = new Appointment {
                Doctor = selectedDoctor,
                Patient = selectedPatient,
                StartDate = startDateTime,
                EndDate = endDateTime,
                Room = emptyRoom,
                AppointmentType = AppointmentType.Regular,
                Anamnesis = "",
                IsDone = false,
                IsRated = false,
                PrescribedMedicines = new List<PrescribedMedicine>()
            };
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
