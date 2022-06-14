using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Persistence;
using HealthInstitution.Model.patient;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Features.UsersManagement.Repository
{
    public class PatientRepository : UserRepository<Patient>, IPatientRepository
    {
        public PatientService(DatabaseContext context) : base(context)
        {

        }
    }
}
