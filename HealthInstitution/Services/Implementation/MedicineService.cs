using HealthInstitution.Model;
using HealthInstitution.Persistence;
using HealthInstitution.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services.Implementation
{
    public class MedicineService : CrudService<Medicine>, IMedicineService
    {
        public MedicineService(DatabaseContext context) : base(context) { }

        public IEnumerable<Medicine> FilterMedicineBySearchText(string searchText)
        {
            searchText = searchText.ToLower();
            return _entities.Where(p => p.Name.ToLower().Contains(searchText) || p.Description.ToLower().Contains(searchText));
        }

        public IEnumerable<Medicine> GetApprovedMedicine()
        {
            return _entities.Where(m => m.Status == Status.Approved);
        }
    }
}
