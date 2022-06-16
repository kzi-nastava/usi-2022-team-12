using System.Collections.Generic;
using HealthInstitution.Core.Features.MedicineManagement.Model;

namespace HealthInstitution.GUI.Features.MedicineManagement
{
    public interface ISearchMedicineViewModel
    {
        public string SearchText { get; set; }
        public IEnumerable<Medicine> Medicines { get; set; }
    }
}
