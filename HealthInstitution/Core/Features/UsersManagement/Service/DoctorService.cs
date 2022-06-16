using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.OffDaysManagement.Repository;
using HealthInstitution.Core.Features.OffDaysManagement.Service;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Core.Features.UsersManagement.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IAppointmentUpdateRequestRepository _appointmentUpdateRequestRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IOffDaysRequestRepository _offDaysRequestRepository;
        private readonly IOffDaysService _offDaysService;

        public DoctorService(IAppointmentRepository appointmentRepository, IOffDaysRequestRepository offDaysRequestRepository, IDoctorRepository doctorRepository, IOffDaysService offDaysService, IAppointmentUpdateRequestRepository appointmentUpdateRequestRepository)
        {
            _offDaysRequestRepository = offDaysRequestRepository;
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
            _offDaysService = offDaysService;
            _appointmentUpdateRequestRepository = appointmentUpdateRequestRepository;
        }

        #region CRUD methods

        public IEnumerable<Doctor> ReadAll()
        {
            return _doctorRepository.ReadAll();
        }

        public Doctor Read(Guid doctorId)
        {
            return _doctorRepository.Read(doctorId);
        }

        public Doctor Create(Doctor newDoctor)
        {
            return _doctorRepository.Create(newDoctor);
        }

        public Doctor Update(Doctor doctorToUpdate)
        {
            return _doctorRepository.Update(doctorToUpdate);
        }

        public Doctor Delete(Guid doctorId)
        {
            return _doctorRepository.Delete(doctorId);
        }

        #endregion

        public IEnumerable<Doctor> FindDoctorsWithSpecialization(DoctorSpecialization specialization)
        {
            return _doctorRepository.ReadAll().Where(doc => doc.Specialization == specialization);
        }

        public IEnumerable<Doctor> FilterDoctorsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            var specializations = Enum.GetValues(typeof(DoctorSpecialization)).Cast<DoctorSpecialization>().Where(text => Enum.GetName(typeof(DoctorSpecialization), text).ToLower().Contains(searchText));
            return _doctorRepository.ReadAll().Where(doc => doc.FirstName.ToLower().Contains(searchText)
           || doc.LastName.ToLower().Contains(searchText) || specializations.Contains(doc.Specialization));
        }

        public bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate)
        {
            return _appointmentRepository.ReadAll().Count(apt => apt.Doctor.Id == doctor.Id && apt.StartDate < toDate && fromDate < apt.EndDate) == 0 &&
                   _offDaysService.IsDoctorInOffice(doctor, fromDate, toDate) && _appointmentUpdateRequestRepository.IsDoctorAvailable(doctor, fromDate, toDate);
        }
    }
}
