using System;
using System.Collections.Generic;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Services.Interfaces
{
    public interface IDoctorService : IUserService<Doctor>
    {
        public IEnumerable<Doctor> FilterDoctorsBySearchText(string searchText);
        public IList<Doctor> GetDoctorsForDoctorSpecialization(DoctorSpecialization doctorSpecialization);
        public IEnumerable<Doctor> FindDoctorsWithSpecialization(DoctorSpecialization specialization);
        public bool IsInOffice(Doctor doctor, DateTime fromDate, DateTime toDate);
    }
}
