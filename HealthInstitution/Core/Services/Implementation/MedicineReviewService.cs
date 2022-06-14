using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Persistence;
using HealthInstitution.Core.Services.Interfaces;

namespace HealthInstitution.Core.Services.Implementation
{
    public class MedicineReviewService : CrudService<MedicineReview>, IMedicineReviewService
    {
        public MedicineReviewService(DatabaseContext context) : base(context) { }
    }
}
