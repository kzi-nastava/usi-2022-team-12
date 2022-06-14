using HealthInstitution.Model.survey;
using HealthInstitution.Model.user;

namespace HealthInstitution.Services.Interfaces
{
    public interface IDoctorSurveyService : ICrudService<DoctorSurvey>
    {
        public double CalculateAvgMark(Doctor doctor);
    }
}
