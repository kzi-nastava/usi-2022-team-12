using System.Collections.Generic;
using HealthInstitution.Core.Features.UsersManagement.Model;

namespace HealthInstitution.Core.Services.Interfaces
{
    public interface IActivityService : ICrudService<Activity>
    {
        IEnumerable<Activity> ReadPatientUpdateOrRemoveActivity(Patient pt, int interval);
        IEnumerable<Activity> ReadPatientMakeActivity(Patient pt, int interval);
    }
}
