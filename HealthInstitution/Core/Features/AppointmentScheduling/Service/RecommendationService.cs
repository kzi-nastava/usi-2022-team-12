using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Service;
using HealthInstitution.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Service
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IAppointmentUpdateRequestRepository _appointmentUpdateRequestRepository;
        private readonly IDoctorService _doctorService;
        private readonly IRoomService _roomService;

        public RecommendationService(IRoomService roomService, IDoctorService doctorService, IAppointmentUpdateRequestRepository appointmentUpdateRequestRepository)
        {
            _doctorService = doctorService;
            _roomService = roomService;
            _appointmentUpdateRequestRepository = appointmentUpdateRequestRepository;
        }

        public Appointment FindFreeAppointmentForDoctorInTimeSpan(Patient patient, List<Doctor> doctors, DateTime intervalStart, DateTime intervalEnd)
        {
            for (DateTime potentialStart = intervalStart; potentialStart.AddMinutes(15) <= intervalEnd; potentialStart = potentialStart.AddMinutes(1))
            {
                DateTime appointmentStart = potentialStart;
                DateTime appointmentEnd = potentialStart.AddMinutes(15);

                foreach (var doctor in doctors)
                {
                    if (!_doctorService.IsDoctorAvailable(doctor, appointmentStart, appointmentEnd) ||
                        !_appointmentUpdateRequestRepository.IsDoctorAvailable(doctor, appointmentStart, appointmentEnd))
                        continue;

                    Room freeRoom = _roomService.FindFreeRoom(RoomType.ExaminationRoom, appointmentStart, appointmentEnd);
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

            if (searchStartDate < today)
            {
                searchStartDate = searchStartDate.AddDays(1);
            }

            for (DateTime potentialDate = searchStartDate; DateOnly.FromDateTime(potentialDate) <= deadline; potentialDate = potentialDate.AddDays(1))
            {
                DateTime dailyIntervalStart = potentialDate.Date.AddTicks(intervalStart.Ticks);
                DateTime dailyIntervalEnd = potentialDate.Date.AddTicks(intervalEnd.Ticks);

                if (intervalStart > intervalEnd)
                {
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
                    if (!_doctorService.IsDoctorAvailable(doctor, appointmentStart, appointmentEnd) ||
                        !_appointmentUpdateRequestRepository.IsDoctorAvailable(doctor, appointmentStart, appointmentEnd))
                        continue;

                    Room freeRoom = _roomService.FindFreeRoom(RoomType.ExaminationRoom, appointmentStart, appointmentEnd);
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
    }
}
