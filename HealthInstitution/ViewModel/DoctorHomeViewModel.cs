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
        public ICommand? LogOutCommand { get; }
        public ICommand? NavigateDoctorScheduleOverviewCommand { get; }

        public DoctorHomeViewModel()
        {
            LogOutCommand = new LogOutCommand();
            NavigateDoctorScheduleOverviewCommand = new SimpleNavigateCommand<DoctorScheduleListingViewModel>();
        }
    }
}
