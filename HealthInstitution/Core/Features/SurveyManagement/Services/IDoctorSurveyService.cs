using HealthInstitution.Core.Features.SurveyManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility;
using System.Collections.Generic;

namespace HealthInstitution.Core.Features.SurveyManagement.Services
{
    public interface IDoctorSurveyService : ICrudService<DoctorSurvey>
    {
        double AverageRecommendation(Doctor doctor);
        double AverageServiceQuality(Doctor doctor);
        double CalculateAvgMark(Doctor doctor);
        List<Doctor> RatedDoctors();
        int RatesPerSurveyCategory(int rate, string cat, Doctor doc);
    }
}