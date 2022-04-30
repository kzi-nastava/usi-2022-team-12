using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.ViewModel
{
    public interface IDoctorAppointmentViewModel
    {
        public void UpdateData(string prefix);
        public string SearchText { get; set; }
    }
}
