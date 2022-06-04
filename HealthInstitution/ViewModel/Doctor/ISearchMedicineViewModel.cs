using System.Collections.Generic;
using HealthInstitution.Model.medicine;

namespace HealthInstitution.ViewModel.doctor
{
    public interface ISearchMedicineViewModel
    {
        public string SearchText { get; set; }
        public IEnumerable<Medicine> Medicines { get; set; }
    }
}
