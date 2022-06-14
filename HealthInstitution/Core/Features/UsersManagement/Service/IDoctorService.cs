using System;
using System.Collections.Generic;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Features.UsersManagement.Service
{
    public interface IDoctorService
    {
        bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate);
        bool IsDoctorAvailableForUpdate(Doctor doctor, DateTime fromDate, DateTime toDate, Appointment aptToUpdate);

        public IList<Doctor> GetDoctorsForDoctorSpecialization(DoctorSpecialization doctorSpecialization);

        public IEnumerable<Doctor> FindDoctorsWithSpecialization(DoctorSpecialization specialization);

        public IEnumerable<Doctor> FilterDoctorsBySearchText(string searchText);
    }
}
