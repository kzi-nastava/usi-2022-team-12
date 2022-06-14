using HealthInstitution.Model.survey;
using HealthInstitution.Model.user;

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
