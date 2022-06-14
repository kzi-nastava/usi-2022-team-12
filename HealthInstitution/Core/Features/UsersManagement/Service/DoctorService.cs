using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;

namespace HealthInstitution.Core.Features.UsersManagement.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
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
    }
}
