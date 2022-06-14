using System.Collections.Generic;
using HealthInstitution.Core.Features.SurveyManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Services.Interfaces
{
    public interface IDoctorSurveyService : ICrudService<DoctorSurvey>
    {
        public double CalculateAvgMark(Doctor doctor);
        public double AverageServiceQuality(Doctor doctor);
        public double AverageRecommendation(Doctor doctor);
        public int RatesPerSurveyCategory(int rate, string cat, Doctor doc);
        public List<Doctor> RatedDoctors();
    }
}
