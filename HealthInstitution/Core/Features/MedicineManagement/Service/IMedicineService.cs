using HealthInstitution.Core.Features.MedicineManagement.Model;
using HealthInstitution.Core.Utility.HelperClasses;
using System.Collections.Generic;

namespace HealthInstitution.Core.Features.MedicineManagement.Service
{
    public interface IMedicineService
    {
        IEnumerable<Medicine> FilterMedicineBySearchText(string searchText, Status medicineStatus);
        IEnumerable<Medicine> GetApprovedMedicine();
        IEnumerable<Medicine> GetMedicineByName(string name);
        IEnumerable<Medicine> GetPendingMedicine();
        IEnumerable<Medicine> GetRejectedMedicine();
        bool MedicineExists(string name);
    }
}