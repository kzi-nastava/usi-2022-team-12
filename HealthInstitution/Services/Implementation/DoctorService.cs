using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services.Implementation
{
    public class DoctorService : CrudService<Doctor>, IDoctorService
    {
        public DoctorService(DatabaseContext context) : base(context)
        {

        }

        public IEnumerable<Doctor> FilterDoctorsBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _entities.Where(p => p.FirstName.ToLower().Contains(searchText)
           || p.LastName.ToLower().Contains(searchText) || (p.Role == Role.Doctor && p.Specialization.ToString().ToLower().Contains(searchText))).ToList();
        }
    }
}
