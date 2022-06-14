using System.Collections.Generic;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Features.UsersManagement.Service
{
    public interface IDoctorService
    {
        public IList<Doctor> GetDoctorsForDoctorSpecialization(DoctorSpecialization doctorSpecialization);

        public IEnumerable<Doctor> FindDoctorsWithSpecialization(DoctorSpecialization specialization);

        public IEnumerable<Doctor> FilterDoctorsBySearchText(string searchText);
    }
}
