using System.Collections.Generic;
using HealthInstitution.Model.patient;
using HealthInstitution.Model.user;

namespace HealthInstitution.Services.Interfaces
{
    public interface IActivityService : ICrudService<Activity>
    {
        IEnumerable<Activity> ReadPatientUpdateOrRemoveActivity(Patient pt, int interval);
        IEnumerable<Activity> ReadPatientMakeActivity(Patient pt, int interval);
    }
}
