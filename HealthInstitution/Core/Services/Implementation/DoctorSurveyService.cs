using HealthInstitution.Model;
using HealthInstitution.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Services.Interfaces;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.SurveyManagement.Model;

namespace HealthInstitution.Services.Implementation
{
    public class DoctorSurveyService : CrudService<DoctorSurvey>, IDoctorSurveyService
    {
        public DoctorSurveyService(DatabaseContext context) : base(context)
        {
            
        }

        public double CalculateAvgMark(Doctor doctor)
        {
            var markSum = (_entities.Where(markObj => markObj.Doctor == doctor).Select(markObj=>markObj.Recommendation)).ToList().DefaultIfEmpty(0).Average()
                + (_entities.Where(markObj => markObj.Doctor == doctor).Select(markObj => markObj.ServiceQuality)).ToList().DefaultIfEmpty(0).Average();
            if (markSum > 0) {
                markSum = markSum / 2;
            }
            return markSum;
        }

        public double AverageServiceQuality(Doctor doctor)
        {
            double avg = _entities.Where(ds => ds.Doctor == doctor).Select(ds => ds.ServiceQuality).ToList().DefaultIfEmpty(0).Average();
            return Math.Round(avg, 2);
        }

        public double AverageRecommendation(Doctor doctor)
        {
            double avg = _entities.Where(ds => ds.Doctor == doctor).Select(his => his.Recommendation).ToList().DefaultIfEmpty(0).Average();
            return Math.Round(avg, 2);
        }

        public int RatesPerSurveyCategory(int rate, string cat, Doctor doc)
        {
            int numOfRates;
            switch (cat)
            {
                case "Service quality":
                    numOfRates = _entities.Where(ds => ds.ServiceQuality == rate && ds.Doctor == doc).Count();
                    break;
                case "Recommendation":
                    numOfRates = _entities.Where(ds => ds.Recommendation == rate && ds.Doctor == doc).Count();
                    break;
                default:
                    numOfRates = _entities.Where(ds => ds.ServiceQuality == rate && ds.Doctor == doc).Count();
                    break;
            }
            return numOfRates;
        }

        public List<Doctor> RatedDoctors()
        {
            return _entities.Select(ds => ds.Doctor).Distinct().ToList();
        }
    }
}
