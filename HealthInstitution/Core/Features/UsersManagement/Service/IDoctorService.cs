using System;
using System.Collections.Generic;
using HealthInstitution.Core.Features.AppointmentScheduling.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.UsersManagement.Service
{
    public interface IDoctorService : ICrudService<Doctor>
    {
        bool IsDoctorAvailable(Doctor doctor, DateTime fromDate, DateTime toDate);

        bool IsDoctorAvailableForUpdate(Doctor doctor, DateTime fromDate, DateTime toDate, Appointment aptToUpdate);

        public IEnumerable<Doctor> FindDoctorsWithSpecialization(DoctorSpecialization specialization);

        public IEnumerable<Doctor> FilterDoctorsBySearchText(string searchText);
    }
}
