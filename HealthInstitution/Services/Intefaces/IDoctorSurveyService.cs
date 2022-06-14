using HealthInstitution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Model.survey;
using HealthInstitution.Model.user;

namespace HealthInstitution.Services.Intefaces
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
