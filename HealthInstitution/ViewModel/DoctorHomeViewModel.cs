using HealthInstitution.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.ViewModel
{
    internal class DoctorHomeViewModel : ViewModelBase
    {
        public ICommand? LogoutCommand { get; }
        public ICommand? NavigateDoctorScheduleOverviewCommand { get; }

        public DoctorHomeViewModel()
        {
            LogoutCommand = new LogoutCommand();

            //TODO Make NavigateCommand template class that should change window to corresponding one 
            //NavigateDoctorScheduleOverviewCommand = new NavigateCommand<>()
        }
    }
}
