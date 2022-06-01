using HealthInstitution.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.ViewModel
{
    public interface ISearchMedicineViewModel
    {
        public string SearchText { get; set; }
        public IEnumerable<Medicine> Medicines { get; set; }
    }
}
