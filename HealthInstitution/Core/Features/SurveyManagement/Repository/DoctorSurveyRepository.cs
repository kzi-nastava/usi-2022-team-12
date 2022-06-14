using HealthInstitution.Core.Features.SurveyManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.SurveyManagement.Repository
{
    public class DoctorSurveyRepository : CrudRepository<DoctorSurvey>, IDoctorSurveyRepository
    {
        public DoctorSurveyRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
