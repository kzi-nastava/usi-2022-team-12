using HealthInstitution.Model.doctor;
using HealthInstitution.Model.user;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services.Implementation
{
    public class OffDaysRequestService : CrudService<OffDaysRequest>, IOffDaysRequestService
    {
        public OffDaysRequestService(DatabaseContext context) : base(context)
        {
        }

        public IEnumerable<OffDaysRequest> GetOffDaysForDoctor(Doctor doctor)
        {
            return _entities.Where(e => e.Doctor == doctor);
        }
    }
}
