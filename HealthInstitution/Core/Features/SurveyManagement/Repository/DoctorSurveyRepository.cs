using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.SurveyManagement.Repository
{
    public class DoctorSurveyRepository : CrudRepository<DoctorSurvey>, IDoctorSurveyRepository
    {
        public DoctorSurveyRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
