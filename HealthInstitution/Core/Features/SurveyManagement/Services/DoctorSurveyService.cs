using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.SurveyManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Features.SurveyManagement.Services
{
    public class DoctorSurveyService
    {
        private readonly IDoctorSurveyRepository _doctorSurveyRepository;

        public DoctorSurveyService(IDoctorSurveyRepository doctorSurveyRepository)
        {
            _doctorSurveyRepository = doctorSurveyRepository;
        }

        public double CalculateAvgMark(Doctor doctor)
        {
            var markSum = (_doctorSurveyRepository.ReadAll().Where(markObj => markObj.Doctor == doctor).Select(markObj => markObj.Recommendation)).ToList().DefaultIfEmpty(0).Average()
                          + (_doctorSurveyRepository.ReadAll().Where(markObj => markObj.Doctor == doctor).Select(markObj => markObj.ServiceQuality)).ToList().DefaultIfEmpty(0).Average();
            if (markSum > 0)
            {
                markSum = markSum / 2;
            }
            return markSum;
        }

        public double AverageServiceQuality(Doctor doctor)
        {
            double avg = _doctorSurveyRepository.ReadAll().Where(ds => ds.Doctor == doctor).Select(ds => ds.ServiceQuality).ToList().DefaultIfEmpty(0).Average();
            return Math.Round(avg, 2);
        }

        public double AverageRecommendation(Doctor doctor)
        {
            double avg = _doctorSurveyRepository.ReadAll().Where(ds => ds.Doctor == doctor).Select(his => his.Recommendation).ToList().DefaultIfEmpty(0).Average();
            return Math.Round(avg, 2);
        }

        public int RatesPerSurveyCategory(int rate, string cat, Doctor doc)
        {
            int numOfRates;
            switch (cat)
            {
                case "Service quality":
                    numOfRates = _doctorSurveyRepository.ReadAll().Where(ds => ds.ServiceQuality == rate && ds.Doctor == doc).Count();
                    break;
                case "Recommendation":
                    numOfRates = _doctorSurveyRepository.ReadAll().Where(ds => ds.Recommendation == rate && ds.Doctor == doc).Count();
                    break;
                default:
                    numOfRates = _doctorSurveyRepository.ReadAll().Where(ds => ds.ServiceQuality == rate && ds.Doctor == doc).Count();
                    break;
            }
            return numOfRates;
        }

        public List<Doctor> RatedDoctors()
        {
            return _doctorSurveyRepository.ReadAll().Select(ds => ds.Doctor).Distinct().ToList();
        }
    }
}
