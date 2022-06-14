using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.MedicalRecordManagement.Repository
{
    public class MedicalRecordRepository : CrudRepository<MedicalRecord>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
