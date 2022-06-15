using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Features.SurveyManagement.Services
{
    public interface IHealthInstitutionSurveyService
    {
        double AverageClearliness();
        double AverageRecommendation();
        double AverageServiceQuality();
        double AverageServiceSatisfaction();
        bool HasPatientAlreadySubmitedSurvey(Patient patient);
        int RatesPerSurveyCategory(int rate, string cat);
    }
}