using System;
using System.Collections.Generic;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.UsersManagement.Service
{
    public interface IActivityService : ICrudService<Activity>
    {
        public IEnumerable<Activity> ReadPatientUpdateOrRemoveActivity(Patient pt, int interval);

        public IEnumerable<Activity> ReadPatientMakeActivity(Patient pt, int interval);

        public int GetNumberOfRecentUpdateOrDeleteActivities(Guid patientId, int interval);

        public int GetNumberOfRecentCreateActivities(Guid patientId, int interval);

        public void ResetActivity(Guid patientId);
    }
}
