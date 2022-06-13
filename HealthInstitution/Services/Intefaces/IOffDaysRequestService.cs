using HealthInstitution.Model;
using HealthInstitution.Model.doctor;
using HealthInstitution.Model.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services.Intefaces
{
    public interface IOffDaysRequestService : ICrudService<OffDaysRequest>
    {
        public IEnumerable<OffDaysRequest> GetOffDaysForDoctor(Doctor doctor);
    }
}
