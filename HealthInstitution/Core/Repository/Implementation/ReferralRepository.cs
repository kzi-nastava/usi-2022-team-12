using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Persistence;
using HealthInstitution.Core.Repository.Interfaces;
using HealthInstitution.Core.Features.OperationsAndExaminations.Model;

namespace HealthInstitution.Core.Repository.Implementation
{
    public class ReferralRepository : CrudRepository<Referral>, IReferralRepository
    {
        public ReferralRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
