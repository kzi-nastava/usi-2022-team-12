using HealthInstitution.Model;
using System.Collections.Generic;

namespace HealthInstitution.Services.Intefaces
{
    public interface IMedicineService : ICrudService<Medicine>
    {
        public IEnumerable<Medicine> GetApprovedMedicine();

        public IEnumerable<Medicine> FilterMedicineBySearchText(string searchText);
    }
}
