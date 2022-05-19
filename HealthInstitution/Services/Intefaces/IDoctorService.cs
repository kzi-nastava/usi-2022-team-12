using HealthInstitution.Model;
using System.Collections.Generic;

namespace HealthInstitution.Services.Intefaces
{
    public interface IDoctorService : IUserService<Doctor>
    {
        public IEnumerable<Doctor> FilterDoctorsBySearchText(string searchText);
        public IList<Doctor> GetDoctorsForDoctorSpecialization(DoctorSpecialization doctorSpecialization);
        public IEnumerable<Doctor> FindDoctorsWithSpecialization(DoctorSpecialization specialization);
        public IEnumerable<Doctor> SearchDoctorsWithFirstName(string searchText);
        public IEnumerable<Doctor> SearchDoctorsWithLastName(string searchText);
        public IEnumerable<Doctor> SearchDoctorsWithSpecializationName(string searchText);
    }
}
