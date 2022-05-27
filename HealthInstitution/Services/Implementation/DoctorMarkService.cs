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
    public class DoctorMarkService : CrudService<DoctorMark>, IDoctorMarkService
    {
        public DoctorMarkService(DatabaseContext context) : base(context)
        {
            
        }

        public double CalculateAvgMark(Doctor doctor)
        {
            return (_entities.Where(markObj => markObj.Doctor == doctor).Select(markObj=>markObj.Mark)).ToList().DefaultIfEmpty(0).Average();
        }
    }
}
