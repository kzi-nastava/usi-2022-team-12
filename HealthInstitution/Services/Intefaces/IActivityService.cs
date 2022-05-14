using HealthInstitution.Model;
using System.Collections.Generic;

namespace HealthInstitution.Services.Intefaces
{
    public interface IActivityService : ICrudService<Activity>
    {
        IEnumerable<Activity> ReadPatientUpdateOrRemoveActivity(Patient pt, int interval);
        IEnumerable<Activity> ReadPatientMakeActivity(Patient pt, int interval);
    }
}
