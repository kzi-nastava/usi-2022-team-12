using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Persistence;
using HealthInstitution.Core.Features.OperationsAndExaminations.Model;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.OperationsAndExaminations.Repository
{
    public class ReferralRepository : CrudRepository<Referral>, IReferralRepository
    {
        public ReferralRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
