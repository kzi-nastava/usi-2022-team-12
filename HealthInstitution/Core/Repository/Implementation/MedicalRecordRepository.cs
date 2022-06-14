using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository.Implementation
{
    public class MedicalRecordRepository : CrudRepository<MedicalRecord>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
