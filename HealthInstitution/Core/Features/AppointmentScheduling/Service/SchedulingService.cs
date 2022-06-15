using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Features.OffDaysManagement.Service;
using HealthInstitution.Core.Features.RoomManagement.Model;
using HealthInstitution.Core.Features.RoomManagement.Service;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Service;
using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Core.Features.AppointmentScheduling.Service
{
    public class SchedulingService : ISchedulingService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IOffDaysService _offDaysService;
        private readonly IRecommendationService _recommendationService;
        private readonly IDoctorService _doctorService;
        private readonly IRoomService _roomService;
        private readonly IAppointmentUpdateRequestRepository _appointmentUpdateRequestRepository;

        public SchedulingService(IAppointmentRepository appointmentRepository,
            IOffDaysService offDaysService, IRecommendationService recommendationService, IDoctorService doctorService, IRoomService roomService, IAppointmentUpdateRequestRepository appointmentUpdateRequestRepository)
        {
            _appointmentRepository = appointmentRepository;
            _offDaysService = offDaysService;
            _recommendationService = recommendationService;
            _doctorService = doctorService;
            _roomService = roomService;
            _appointmentUpdateRequestRepository = appointmentUpdateRequestRepository;
        }

        public List<Appointment> RecommendAppointments(Patient patient, Doctor doctor, TimeOnly startTime, TimeOnly endTime, DateOnly deadline, string priority)
        {
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

        public void MakeAppointment(Patient selectedPatient, Doctor selectedDoctor, DateTime startDateTime, DateTime endDateTime, AppointmentType appointmentType)
        {
            //doctor availabilty check
            if (!_doctorService.IsDoctorAvailable(selectedDoctor, startDateTime, endDateTime) ||
                !_appointmentUpdateRequestRepository.IsDoctorAvailable(selectedDoctor, startDateTime, endDateTime))
            {
                throw new DoctorBusyException();
            }

            //room availabilty check
            RoomType roomType = appointmentType == AppointmentType.Regular ? RoomType.ExaminationRoom : RoomType.OperationRoom;
            Room emptyRoom = _roomService.FindFreeRoom(roomType, startDateTime, endDateTime);
            if (emptyRoom == null)
            {
                throw new RoomBusyException();
            }

            Appointment app = new Appointment
            {
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
            _appointmentRepository.Create(app);
        }

        public bool UpdateAppointment(Appointment selectedAppointment, Patient selectedPatient, Doctor selectedDoctor, DateTime startDateTime, DateTime endDateTime)
        {
            if (!_doctorService.IsDoctorAvailableForUpdate(selectedDoctor, startDateTime, endDateTime, selectedAppointment) ||
                !_appointmentUpdateRequestRepository.IsDoctorAvailable(selectedDoctor, startDateTime, endDateTime))
            {
                throw new DoctorBusyException();
            }

            //rooms availabilty check
            Room emptyRoom = _roomService.FindFreeRoom(RoomType.ExaminationRoom, startDateTime, endDateTime);
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
                _appointmentUpdateRequestRepository.Create(appointmentRequest);
                return false;
            }

            selectedAppointment.StartDate = startDateTime;
            selectedAppointment.EndDate = endDateTime;
            selectedAppointment.Doctor = selectedDoctor;
            selectedAppointment.Room = emptyRoom;
            _appointmentRepository.Update(selectedAppointment);

            return true;
        }
    }
}
