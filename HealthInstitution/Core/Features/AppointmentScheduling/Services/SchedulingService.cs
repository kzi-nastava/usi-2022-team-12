using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.OffDaysManagement.Services;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Repository;
using HealthInstitution.Core.Features.RoomManagement.Services;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Services
{
    public class SchedulingService : ISchedulingService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IRoomRenovationRepository _roomRenovationRepository;
        private readonly IAppointmentUpdateRequestRepository _appointmentUpdateRequestRepository;
        private readonly IOffDaysService _offDaysService;
        private readonly IRecommendationService _recommendationService;
        private readonly IRoomService _roomService;

        public SchedulingService(IAppointmentRepository appointmentRepository,
            IOffDaysService offDaysService, IRecommendationService recommendationService, 
            IRoomService roomService, IRoomRenovationRepository roomRenovationRepository,
            IAppointmentUpdateRequestRepository appointmentUpdateRequestRepository)
        {
            _appointmentRepository = appointmentRepository;
            _offDaysService = offDaysService;
            _recommendationService = recommendationService;
            _roomService = roomService;
            _roomRenovationRepository = roomRenovationRepository;
            _appointmentUpdateRequestRepository = appointmentUpdateRequestRepository;
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

        public IEnumerable<Appointment> ReadFinishedAppointmentsForPatient(Patient pt)
        {
            return _appointmentRepository.ReadAll().Where(ap => ap.IsDone == true)
                            .Where(apt => apt.Patient == pt);
        }

        public IEnumerable<Appointment> ReadPatientAppointments(Patient pt)
        {
            return _appointmentRepository.ReadAll().Where(apt => apt.Patient == pt);
        }

        public IEnumerable<Appointment> ReadFuturePatientAppointments(Patient pt)
        {
            return _appointmentRepository.ReadAll().Where(apt => apt.Patient == pt && apt.StartDate > DateTime.Now && apt.IsDone == false);
        }

        public IEnumerable<Appointment> ReadRoomAppointments(Room r)
        {
            return _appointmentRepository.ReadAll().Where(apt => apt.Room == r).ToList();
        }

        public Room FindFreeRoom(RoomType roomType, DateTime start, DateTime end)
        {
            var examinationRooms = _roomService.ReadRooms(roomType);
            foreach (var room in examinationRooms)
            {
                if (IsRoomAvailable(room, start, end) && _appointmentUpdateRequestRepository.IsRoomAvailable(room, start, end)
                    && _roomRenovationRepository.IsRoomNotRenovating(room, start, end))
                {
                    return room;
                }
            }
            return null;
        }
    }
}
