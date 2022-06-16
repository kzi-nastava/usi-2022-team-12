using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Features.UsersManagement.Model;
using HealthInstitution.Core.Features.UsersManagement.Repository;

namespace HealthInstitution.Core.Features.UsersManagement.Service
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        #region CRUD methods

        public IEnumerable<Activity> ReadAll()
        {
            return _activityRepository.ReadAll();
        }

        public Activity Read(Guid activityId)
        {
            return _activityRepository.Read(activityId);
        }

        public Activity Create(Activity newActivity)
        {
            return _activityRepository.Create(newActivity);
        }

        public Activity Update(Activity activityToUpdate)
        {
            return _activityRepository.Update(activityToUpdate);
        }

        public Activity Delete(Guid activityId)
        {
            return _activityRepository.Delete(activityId);
        }

        #endregion

        public IEnumerable<Activity> ReadPatientUpdateOrRemoveActivity(Patient pt, int interval)
        {
            return _activityRepository.ReadAll().Where(act => act.Patient.Id == pt.Id && act.DateOfAction > DateTime.Now.AddDays(-interval) && act.ActivityType != ActivityType.Create);
        }

        public IEnumerable<Activity> ReadPatientMakeActivity(Patient pt, int interval)
        {
            return _activityRepository.ReadAll().Where(act => act.Patient.Id == pt.Id && act.DateOfAction > DateTime.Now.AddDays(-interval) && act.ActivityType == ActivityType.Create);
        }

        public int GetNumberOfRecentUpdateOrDeleteActivities(Guid patientId, int interval)
        {
            return _activityRepository.ReadAll()
                .Where(act => act.Patient.Id == patientId)
                .Where(act => act.ActivityType == ActivityType.Update || act.ActivityType == ActivityType.Delete)
                .Where(act => act.DateOfAction > DateTime.Now.AddDays(-interval))
                .Count(act => act.IsRelevantForBan);
        }

        public int GetNumberOfRecentCreateActivities(Guid patientId, int interval)
        {
            return _activityRepository.ReadAll()
                .Where(act => act.Patient.Id == patientId)
                .Where(act => act.ActivityType == ActivityType.Create)
                .Where(act => act.DateOfAction > DateTime.Now.AddDays(-interval))
                .Count(act => act.IsRelevantForBan);
        }

        public void ResetActivity(Guid patientId)
        {
            var activities = _activityRepository.ReadAll().Where(act => act.Patient.Id == patientId);
            foreach (var activity in activities)
            {
                if (!activity.IsRelevantForBan) continue;

                activity.IsRelevantForBan = false;
                Update(activity);
            }
        }
    }
}
