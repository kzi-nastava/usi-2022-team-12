using System.Collections.Generic;
using HealthInstitution.Model;
using HealthInstitution.Model.medicine;

namespace HealthInstitution.Services.Interfaces
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
