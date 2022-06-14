using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.UsersManagement.Repository
{
    public class DoctorRepository : UserRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
