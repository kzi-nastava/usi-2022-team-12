using HealthInstitution.Core.Features.SurveyManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.SurveyManagement.Services
{
    public interface IHealthInstitutionSurveyService : ICrudService<HealthInstitutionSurvey>
    {
        double AverageClearliness();
        double AverageRecommendation();
        double AverageServiceQuality();
        double AverageServiceSatisfaction();
        bool HasPatientAlreadySubmitedSurvey(Patient patient);
        int RatesPerSurveyCategory(int rate, string cat);
    }
}