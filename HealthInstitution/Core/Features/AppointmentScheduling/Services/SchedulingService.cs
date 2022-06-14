using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.OffDaysManagement.Services;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Services
{
    public class SchedulingService : ISchedulingService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        private readonly IDoctorRepository _doctorRepository;

        private readonly IRoomRepository _roomRepository;

        private readonly IOffDaysService _offDaysService;

        public SchedulingService(IAppointmentRepository appointmentRepository,
            IDoctorRepository doctorRepository, IRoomRepository roomRepository,
            IOffDaysService offDaysService)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _roomRepository = roomRepository;
            _offDaysService = offDaysService;
        }

        public bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate)
        {
            return _appointmentRepository.ReadAll().Count(apt => apt.Doctor == doctor && apt.StartDate < toDate && fromDate < apt.EndDate) == 0 &&
                   _offDaysService.IsDoctorInOffice(doctor, fromDate, toDate);
        }

        public bool IsDoctorAvailableForUpdate(Doctor doctor, DateTime fromDate, DateTime toDate, Appointment aptToUpdate)
        {
            return _appointmentRepository.ReadAll().Count(apt => apt != aptToUpdate && apt.Doctor == doctor && apt.StartDate < toDate && fromDate < apt.EndDate) == 0;
        }

        public bool IsRoomAvailable(Room room, DateTime fromDate, DateTime toDate)
        {
            return _appointmentRepository.ReadAll().Count(apt => apt.Room == room && apt.StartDate < toDate && fromDate < apt.EndDate) == 0;
        }

        public bool IsRoomAvailableForUpdate(Room room, DateTime fromDate, DateTime toDate, Appointment aptToUpdate)
        {
            return _appointmentRepository.ReadAll().Count(apt => apt.Room == room && apt != aptToUpdate && apt.StartDate < toDate && fromDate < apt.EndDate) == 0;
        }

        public IEnumerable<Appointment> GetAppointmentsForDateRangeAndDoctor(DateTime start, DateTime end, Doctor doctor)
        {
            return _appointmentRepository.ReadAll().Where(e => e.Doctor == doctor && e.StartDate.Date >= start.Date && e.StartDate.Date <= end.Date);
        }
    }
}
