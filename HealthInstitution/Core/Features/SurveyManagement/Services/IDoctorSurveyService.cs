using HealthInstitution.Core.Features.SurveyManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Model;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Features.SurveyManagement.Services
{
    public interface IDoctorSurveyService
    {
        double AverageRecommendation(Doctor doctor);
        double AverageServiceQuality(Doctor doctor);
        double CalculateAvgMark(Doctor doctor);
        DoctorSurvey Create(DoctorSurvey newDoctorSurvey);
        DoctorSurvey Delete(Guid doctorSurveyId);
        List<Doctor> RatedDoctors();
        int RatesPerSurveyCategory(int rate, string cat, Doctor doc);
        DoctorSurvey Read(Guid doctorSurveyId);
        IEnumerable<DoctorSurvey> ReadAll();
        DoctorSurvey Update(DoctorSurvey doctorSurveyToUpdate);
    }
}