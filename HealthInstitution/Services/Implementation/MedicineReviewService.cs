using HealthInstitution.Model;
using HealthInstitution.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Model.medicine;
using HealthInstitution.Services.Interfaces;

namespace HealthInstitution.Services.Implementation
{
    public class MedicineReviewService : CrudService<MedicineReview>, IMedicineReviewService
    {
        public MedicineReviewService(DatabaseContext context) : base(context) { }
    }
}
