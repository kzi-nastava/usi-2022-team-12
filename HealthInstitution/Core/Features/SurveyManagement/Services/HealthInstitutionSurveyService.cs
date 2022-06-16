using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.SurveyManagement.Model;
using HealthInstitution.Core.Features.SurveyManagement.Repository;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Features.SurveyManagement.Services
{
    public class HealthInstitutionSurveyService : IHealthInstitutionSurveyService
    {
        private readonly IHealthInstitutionSurveyRepository _healthInstitutionSurveyRepository;

        public HealthInstitutionSurveyService(IHealthInstitutionSurveyRepository healthInstitutionSurveyRepository)
        {
            _healthInstitutionSurveyRepository = healthInstitutionSurveyRepository;
        }

        #region CRUD methods

        public IEnumerable<HealthInstitutionSurvey> ReadAll()
        {
            return _healthInstitutionSurveyRepository.ReadAll();
        }

        public HealthInstitutionSurvey Read(Guid healthInstitutionSurveyId)
        {
            return _healthInstitutionSurveyRepository.Read(healthInstitutionSurveyId);
        }

        public HealthInstitutionSurvey Create(HealthInstitutionSurvey newHealthInstitutionSurvey)
        {
            return _healthInstitutionSurveyRepository.Create(newHealthInstitutionSurvey);
        }

        public HealthInstitutionSurvey Update(HealthInstitutionSurvey healthInstitutionSurveyToUpdate)
        {
            return _healthInstitutionSurveyRepository.Update(healthInstitutionSurveyToUpdate);
        }

        public HealthInstitutionSurvey Delete(Guid healthInstitutionSurveyId)
        {
            return _healthInstitutionSurveyRepository.Delete(healthInstitutionSurveyId);
        }

        #endregion

        public bool HasPatientAlreadySubmitedSurvey(Patient patient)
        {
            return _healthInstitutionSurveyRepository.ReadAll().Where(his => his.Patient == patient && his.CreatedAt.AddDays(30) > DateTime.Now).ToList().Count != 0;
        }

        public double AverageClearliness()
        {
            double avg = _healthInstitutionSurveyRepository.ReadAll().Select(his => his.Cleanliness).ToList().DefaultIfEmpty(0).Average();
            return Math.Round(avg, 2);
        }

        public double AverageServiceQuality()
        {
            double avg = _healthInstitutionSurveyRepository.ReadAll().Select(his => his.ServiceQuality).ToList().DefaultIfEmpty(0).Average();
            return Math.Round(avg, 2);
        }

        public double AverageServiceSatisfaction()
        {
            double avg = _healthInstitutionSurveyRepository.ReadAll().Select(his => his.ServiceSatisfaction).ToList().DefaultIfEmpty(0).Average();
            return Math.Round(avg, 2);
        }

        public double AverageRecommendation()
        {
            double avg = _healthInstitutionSurveyRepository.ReadAll().Select(his => his.Recommendation).ToList().DefaultIfEmpty(0).Average();
            return Math.Round(avg, 2);
        }

        public int RatesPerSurveyCategory(int rate, string cat)
        {
            int numOfRates;
            switch (cat)
            {
                case "Clearliness":
                    numOfRates = _healthInstitutionSurveyRepository.ReadAll().Count(his => his.Cleanliness == rate);
                    break;
                case "Service quality":
                    numOfRates = _healthInstitutionSurveyRepository.ReadAll().Count(his => his.ServiceQuality == rate);
                    break;
                case "Service satisfaction":
                    numOfRates = _healthInstitutionSurveyRepository.ReadAll().Count(his => his.ServiceSatisfaction == rate);
                    break;
                case "Recommendation":
                    numOfRates = _healthInstitutionSurveyRepository.ReadAll().Count(his => his.Recommendation == rate);
                    break;
                default:
                    numOfRates = _healthInstitutionSurveyRepository.ReadAll().Count(his => his.Cleanliness == rate);
                    break;
            }
            return numOfRates;
        }
    }
}
