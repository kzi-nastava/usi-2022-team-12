using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository.Interfaces
{
    public interface IPatientRepository : ICrudRepository<PrescribedMedicineNotification>
    {
    }
}
