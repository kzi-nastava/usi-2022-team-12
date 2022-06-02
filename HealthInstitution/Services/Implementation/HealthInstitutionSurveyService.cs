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
    public class HealthInstitutionSurveyService : CrudService<HealthInstitutionSurvey>, IHealthInstitutionSurveyService
    {
        public HealthInstitutionSurveyService(DatabaseContext context) : base(context)
        {

        }

        public bool HasPatientAlreadySubmitedSurvey(Patient patient) {
            return _entities.Where(his => his.Patient == patient && his.CreatedAt.AddDays(30) > DateTime.Now).ToList().Count != 0;
        }
    }
}
