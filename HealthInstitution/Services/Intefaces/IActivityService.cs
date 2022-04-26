using HealthInstitution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services.Intefaces
{
    public interface IActivityService : ICrudService<Activity>
    {
        IEnumerable<Activity> ReadPatientUpdateOrRemoveActivity(Patient pt, int interval);
        IEnumerable<Activity> ReadPatientMakeActivity(Patient pt, int interval);
    }
}
