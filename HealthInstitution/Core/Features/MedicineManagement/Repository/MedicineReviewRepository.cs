using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.MedicineManagement.Repository
{
    public class MedicineReviewRepository : CrudRepository<MedicineReview>, IMedicineReviewRepository
    {
        public MedicineRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
