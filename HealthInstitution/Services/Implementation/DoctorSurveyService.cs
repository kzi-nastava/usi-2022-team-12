using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services.Implementation
{
    public class DoctorSurveyService : CrudService<DoctorSurvey>, IDoctorSurveyService
    {
        public DoctorSurveyService(DatabaseContext context) : base(context)
        {
            
        }

        public double CalculateAvgMark(Doctor doctor)
        {
            var markSum = (_entities.Where(markObj => markObj.Doctor == doctor).Select(markObj=>markObj.WouldRecommend)).ToList().DefaultIfEmpty(0).Average()
                + (_entities.Where(markObj => markObj.Doctor == doctor).Select(markObj => markObj.ServiceQuality)).ToList().DefaultIfEmpty(0).Average();
            if (markSum > 0) {
                markSum = markSum / 2;
            }
            return markSum;
        }
    }
}
