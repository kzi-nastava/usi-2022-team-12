using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.OffDaysManagement.Service;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Repository;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Service
{
    public class SchedulingService : ISchedulingService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IOffDaysService _offDaysService;
        private readonly IRecommendationService _recommendationService;

        public SchedulingService(IAppointmentRepository appointmentRepository,
            IOffDaysService offDaysService, IRecommendationService recommendationService)
        {
            _appointmentRepository = appointmentRepository;
            _offDaysService = offDaysService;
            _recommendationService = recommendationService;
        }

        public List<Appointment> RecommendAppointments(Patient patient, Doctor doctor, TimeOnly startTime, TimeOnly endTime, DateOnly deadline, string priority) {
            return _recommendationService.RecommendAppointments(patient, doctor, startTime, endTime, deadline, priority);
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

        public IEnumerable<Appointment> FindFinishedAppointmentsWithAnamnesis(Patient patient, string searchText)
        {
            searchText = searchText.ToLower();
            return _appointmentRepository.ReadAll().Where(apt => apt.Anamnesis.ToLower().Contains(searchText) && apt.IsDone == true && apt.Patient == patient);
        }

        public bool PatientHasAnAppointment(Guid patientId)
        {
            return (_appointmentRepository.ReadAll().Where(apt => apt.Patient.Id == patientId).Count() != 0);
        }
    }
}
