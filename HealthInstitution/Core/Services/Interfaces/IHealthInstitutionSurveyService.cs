using HealthInstitution.Core.Features.SurveyManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Services.Interfaces
{
    public interface IHealthInstitutionSurveyService : ICrudService<HealthInstitutionSurvey>
    {
        public bool HasPatientAlreadySubmitedSurvey(Patient patient);
        public double AverageClearliness();
        public double AverageServiceQuality();
        public double AverageServiceSatisfaction();
        public double AverageRecommendation();
        public int RatesPerSurveyCategory(int rate, string cat);
    }
}
