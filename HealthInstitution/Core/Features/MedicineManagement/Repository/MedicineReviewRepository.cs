using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Utility;

namespace HealthInstitution.Core.Features.MedicineManagement.Repository
{
    public class MedicineReviewRepository : CrudRepository<MedicineReview>, IMedicineReviewRepository
    {
        public MedicineReviewRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
