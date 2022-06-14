using HealthInstitution.Core.Features.SurveyManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.SurveyManagement.Repository
{
    public class HealthInstitutionSurveyRepository : CrudRepository<HealthInstitutionSurvey>, IHealthInstitutionSurveyRepository
    {
        public HealthInstitutionSurveyRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
