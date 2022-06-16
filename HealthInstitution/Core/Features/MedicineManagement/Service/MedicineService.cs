using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Features.MedicineManagement.Repository;
using HealthInstitution.Core.Utility.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Features.MedicineManagement.Service
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;
        public MedicineService(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }
        public IEnumerable<Medicine> FilterMedicineBySearchText(string searchText, Status medicineStatus)
        {
            searchText = searchText.ToLower();
            return _medicineRepository.ReadAll().Where(p => p.Name.ToLower().Contains(searchText) || p.Description.ToLower().Contains(searchText))
                .Where(p => p.Status == medicineStatus);
        }

        public IEnumerable<Medicine> GetApprovedMedicine()
        {
            return _medicineRepository.ReadAll().Where(m => m.Status == Status.Approved);
        }

        public IEnumerable<Medicine> GetPendingMedicine()
        {
            return _medicineRepository.ReadAll().Where(m => m.Status == Status.Pending);
        }

        public IEnumerable<Medicine> GetRejectedMedicine()
        {
            return _medicineRepository.ReadAll().Where(m => m.Status == Status.Rejected);
        }

        public bool MedicineExists(string name)
        {
            return _medicineRepository.ReadAll().Where(m => m.Name.ToLower() == name.ToLower() && m.Status == Status.Approved).Count() != 0;
        }

        public IEnumerable<Medicine> GetMedicineByName(string name)
        {
            return _medicineRepository.ReadAll().Where(m => m.Name == name);
        }
    }
}
