using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthInstitution.Services.Implementation
{
    public class DoctorService : UserService<Doctor>, IDoctorService
    {
        public DoctorService(DatabaseContext context) : base(context)
        {
        }

        public IList<Doctor> GetDoctorsForDoctorSpecialization(DoctorSpecialization doctorSpecialization)
        {
            return _entities.Where(d => d.Specialization == doctorSpecialization).ToList();
            
        }

        public IEnumerable<Doctor> FindDoctorsWithSpecialization(DoctorSpecialization specialization)
        {
            return _entities.Where(doc => doc.Specialization == specialization);
        }

        public IEnumerable<Doctor> FilterDoctorsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _entities.Where(p => p.FirstName.ToLower().Contains(searchText)
           || p.LastName.ToLower().Contains(searchText));
        }
    }
}
