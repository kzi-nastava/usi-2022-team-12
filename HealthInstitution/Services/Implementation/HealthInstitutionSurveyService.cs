using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Model.survey;
using HealthInstitution.Model.user;

namespace HealthInstitution.Services.Implementation
{
    public class HealthInstitutionSurveyService : CrudService<HealthInstitutionSurvey>, IHealthInstitutionSurveyService
    {
        public HealthInstitutionSurveyService(DatabaseContext context) : base(context)
        {

        }

        public bool HasPatientAlreadySubmitedSurvey(Patient patient) {
            return _entities.Where(his => his.Patient == patient && his.CreatedAt.AddDays(30) > DateTime.Now).ToList().Count != 0;
        }

        public double AverageClearliness()
        {
            return _entities.Average(his => his.Cleanliness);
        }

        public double AverageServiceQuality()
        {
            return _entities.Average(his => his.ServiceQuality);
        }

        public double AverageServiceSatisfaction()
        {
            return _entities.Average(his => his.ServiceSatisfaction);
        }

        public double AverageRecommendation()
        {
            return _entities.Average(his => his.Recommendation);
        }

        public int RatesPerSurveyCategory(int rate, string cat)
        {
            int numOfRates;
            switch (cat)
            {
                case "Clearliness":
                    numOfRates = _entities.Where(his => his.Cleanliness == rate).Count();
                break;
                case "Service quality":
                    numOfRates = _entities.Where(his => his.ServiceQuality == rate).Count();
                break;
                case "Service satisfaction":
                    numOfRates = _entities.Where(his => his.ServiceSatisfaction == rate).Count();
                break;
                case "Recommendation":
                    numOfRates = _entities.Where(his => his.Recommendation == rate).Count();
                break;
                default:
                    numOfRates = _entities.Where(his => his.Cleanliness == rate).Count();
                break;
            }
            return numOfRates;
        }
    }
}
