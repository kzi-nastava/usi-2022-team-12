using System.Collections.Generic;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Features.SurveyManagement.Services
{
    public interface IDoctorSurveyService
    {
        public double CalculateAvgMark(Doctor doctor);

        public double AverageServiceQuality(Doctor doctor);

        public double AverageRecommendation(Doctor doctor);

        public int RatesPerSurveyCategory(int rate, string cat, Doctor doctor);

        public List<Doctor> RatedDoctors();
    }
}
