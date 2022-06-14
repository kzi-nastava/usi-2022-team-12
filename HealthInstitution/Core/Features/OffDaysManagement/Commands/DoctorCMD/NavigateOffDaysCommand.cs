using HealthInstitution.GUI.Utility.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core.Utility.Command;

namespace HealthInstitution.Core.Features.OffDaysManagement.Commands.DoctorCMD
{
    public class NavigateOffDaysCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            EventBus.FireEvent("DoctorOffDays");
        }
    }
}
