﻿using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.AppointmentScheduling.Repository;
using HealthInstitution.Core.Features.OffDaysManagement.Repository;
using HealthInstitution.Core.Features.OffDaysManagement.Services;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;
using HealthInstitution.Core.Utility.HelperClasses;

namespace HealthInstitution.Core.Features.UsersManagement.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IOffDaysRequestRepository _offDaysRequestRepository;
        private readonly IOffDaysService _offDaysService;

        public DoctorService(IAppointmentRepository appointmentRepository, IOffDaysRequestRepository offDaysRequestRepository, IDoctorRepository doctorRepository, IOffDaysService offDaysService)
        {
            _offDaysRequestRepository = offDaysRequestRepository;
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
            _offDaysService = offDaysService;
        }

        public IList<Doctor> GetDoctorsForDoctorSpecialization(DoctorSpecialization doctorSpecialization)
        {
            return _doctorRepository.ReadAll().Where(d => d.Specialization == doctorSpecialization).ToList();

        }

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

        public bool IsInOffice(Doctor doctor, DateTime fromDate, DateTime toDate)
        {
            return _offDaysRequestRepository.ReadAll()
                .Where(e => e.Doctor == doctor)
                .Where(e => e.Status == Status.Approved)
                .Count(e => e.StartDate <= toDate && fromDate <= e.EndDate) == 0;
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
    }
}