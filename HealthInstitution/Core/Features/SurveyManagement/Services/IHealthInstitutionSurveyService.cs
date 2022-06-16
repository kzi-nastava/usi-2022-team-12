using HealthInstitution.Core.Features.SurveyManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Features.SurveyManagement.Services
{
    public interface IHealthInstitutionSurveyService
    {
        double AverageClearliness();
        double AverageRecommendation();
        double AverageServiceQuality();
        double AverageServiceSatisfaction();
        HealthInstitutionSurvey Create(HealthInstitutionSurvey newHealthInstitutionSurvey);
        HealthInstitutionSurvey Delete(Guid healthInstitutionSurveyId);
        bool HasPatientAlreadySubmitedSurvey(Patient patient);
        int RatesPerSurveyCategory(int rate, string cat);
        HealthInstitutionSurvey Read(Guid healthInstitutionSurveyId);
        IEnumerable<HealthInstitutionSurvey> ReadAll();
        HealthInstitutionSurvey Update(HealthInstitutionSurvey healthInstitutionSurveyToUpdate);
    }
}