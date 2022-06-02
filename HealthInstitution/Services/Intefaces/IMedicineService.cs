using HealthInstitution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services.Intefaces
{
    public interface IMedicineService : ICrudService<Medicine>
    {
        public IEnumerable<Medicine> GetApprovedMedicine();
        public IEnumerable<Medicine> GetPendingMedicine();
        public IEnumerable<Medicine> GetRejectedMedicine();
        public IEnumerable<Medicine> FilterMedicineBySearchText(string searchText, Status medicineStatus);
        public bool MedicineExists(string name);
        public IEnumerable<Medicine> GetMedicineByName(string name);
    }
}
