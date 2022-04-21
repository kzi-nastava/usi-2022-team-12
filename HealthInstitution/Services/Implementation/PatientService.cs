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
    public class PatientService<T> : CrudService<T>, IPatientService<T> where T : Patient
    {
        public PatientService(DatabaseContext context) : base(context)
        {

        }
    }
}
